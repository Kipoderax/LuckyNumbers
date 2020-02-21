using System.Collections.Generic;
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

        public async Task<User> getUser(string username)
        {
            var user = await context.users.Include(l => l.userExperience)
                                          .Include(l => l.lottoGame)
                                          .FirstOrDefaultAsync(u => u.username == username);
            return user;
        }

        public async Task<IEnumerable<User>> getUsers()
        {
            var users = await context.users.Include(u => u.userExperience)
                                           .Include(l => l.lottoGame)
                                           .ToListAsync();

            return users;
        }

        public async Task<List<int>> getBetsSended ( ) {
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
    }
}