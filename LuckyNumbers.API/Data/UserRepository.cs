using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly DataContext context;
        public UserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> getUserByUsername(string username)
        {
            var user = await context.users.Include(l => l.userExperience)
                                          .Include(l => l.lottoGame)
                                          .FirstOrDefaultAsync(u => u.username == username);
            return user;
        }

        public async Task<User> getUserByUserId ( int userId ) {
            var user = await context.users.Include( l => l.userExperience )
                                          .Include( l => l.lottoGame )
                                          .Include( l => l.lottoHistoryGames )
                                          .FirstOrDefaultAsync( u => u.userId == userId );
            return user;
        }

        public async Task<IEnumerable<User>> getUsers()
        {
            var users = await context.users.Include(u => u.userExperience)
                                           .Include(l => l.lottoGame)
                                           .OrderByDescending(l => l.userExperience.level)
                                           .ToListAsync();

            return users;
        }

        public async Task<List<int>> serverStatus ( ) {
            List<int> status = new List<int>();

            var registeredUsers = await context.users.CountAsync();
            status.Add( registeredUsers );

            var betsSended = await context.users.Include( l => l.lottoGame ).SumAsync( b => b.lottoGame.betsSended );
            status.Add( betsSended );

            var amountOfThree = await context.users.Include( l => l.lottoGame ).SumAsync( b => b.lottoGame.amountOfThree );
            status.Add( amountOfThree );

            var amountOfFour = await context.users.Include( l => l.lottoGame ).SumAsync( b => b.lottoGame.amountOfFour );
            status.Add( amountOfFour );

            var amountOfFive = await context.users.Include( l => l.lottoGame ).SumAsync( b => b.lottoGame.amountOfFive );
            status.Add( amountOfFive );

            var amountOfSix = await context.users.Include( l => l.lottoGame ).SumAsync( b => b.lottoGame.amountOfSix );
            status.Add( amountOfSix );

            return status;
        }

        public async Task<IEnumerable<User>> best5Players() {
            var users = context.users.Include( u => u.userExperience )
                .OrderByDescending( l => l.userExperience.level )
                .ThenByDescending(e => e.userExperience.experience).Take(5)
                .ToListAsync();

            return await users;
        }

        public async Task<IEnumerable<HistoryGameForLotto>> top5Xp() {
            var user = context.lottoHistoryGames.Include( u => u.user )
                .OrderByDescending( x => x.experience ).Take(5)
                .ToListAsync();

            return await user;
        }

        public async Task<IEnumerable<HistoryGameForLotto>> userHistoryGame(string username) {

            var user = context.lottoHistoryGames.Include( u => u.user )
                .Where( u => u.user.username == username )
                .ToListAsync();

            return await user;
        }

        public async Task<IEnumerable<UserLottoBets>> userSendedBets(string username) {

            var user = context.userLottoBets.Include(u => u.user)
                .Where(u => u.user.username == username)
                .ToListAsync();

            return await user;
        }
    }
}