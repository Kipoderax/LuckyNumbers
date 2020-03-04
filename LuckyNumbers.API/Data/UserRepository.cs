using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using LuckyNumbers.API.Service;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public UserRepository(DataContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<User> getUserByUsername(string username)
        {
            var user = await context.users.Include(l => l.userExperience)
                                          .Include(l => l.lottoGame)
                                          .FirstOrDefaultAsync(u => u.username == username);
            return user;
        }

        public async Task<User> getUserByUserId(int userId)
        {
            var user = await context.users.Include(l => l.userExperience)
                                          .Include(l => l.lottoGame)
                                          .Include(l => l.lottoHistoryGames)
                                          .FirstOrDefaultAsync(u => u.userId == userId);
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

        public async Task<List<int>> serverStatus()
        {
            List<int> status = new List<int>();

            var registeredUsers = await context.users.CountAsync();
            status.Add(registeredUsers);

            var betsSended = await context.users.Include(l => l.lottoGame).SumAsync(b => b.lottoGame.betsSended);
            status.Add(betsSended);

            var amountOfThree = await context.users.Include(l => l.lottoGame).SumAsync(b => b.lottoGame.amountOfThree);
            status.Add(amountOfThree);

            var amountOfFour = await context.users.Include(l => l.lottoGame).SumAsync(b => b.lottoGame.amountOfFour);
            status.Add(amountOfFour);

            var amountOfFive = await context.users.Include(l => l.lottoGame).SumAsync(b => b.lottoGame.amountOfFive);
            status.Add(amountOfFive);

            var amountOfSix = await context.users.Include(l => l.lottoGame).SumAsync(b => b.lottoGame.amountOfSix);
            status.Add(amountOfSix);

            return status;
        }

        public async Task<IEnumerable<User>> best5Players()
        {
            var users = context.users.Include(u => u.userExperience)
                .OrderByDescending(l => l.userExperience.level)
                .ThenByDescending(e => e.userExperience.experience).Take(5)
                .ToListAsync();

            return await users;
        }

        public async Task<IEnumerable<HistoryGameForLotto>> top5Xp()
        {
            var user = context.lottoHistoryGames.Include(u => u.user)
                .OrderByDescending(x => x.experience).Take(5)
                .ToListAsync();

            return await user;
        }

        public async Task<IEnumerable<HistoryGameForLotto>> userHistoryGame(string username)
        {

            var user = context.lottoHistoryGames.Include(u => u.user)
                .Where(u => u.user.username == username).OrderByDescending(u => u.historyLottoId)
                .ToListAsync();

            return await user;
        }

        public async Task<IEnumerable<UserLottoBets>> userSendedBets(int userId)
        {

            var user = context.userLottoBets.Include(u => u.user)
                .Where(u => u.user.userId == userId)
                .ToListAsync();

            return await user;
        }

        public async void deleteSendedBets(UserLottoBets userLottoBets, int userId) {

            var userFromRepo = await getUserByUserId(userId);
            int count = userFromRepo.userlottoBets.Count;

            while(count > 0) {
                var numbers = context.userLottoBets.Include(u => u.user).Where(u => u.userId == userId);
                context.userLottoBets.RemoveRange(numbers);
                count--;
            }
        }

        public int getLastBetId() {
            int lastId = this.context.userLottoBets.Select(u => u.lottoBetsId).Max();

            return lastId;
        }
    }
}