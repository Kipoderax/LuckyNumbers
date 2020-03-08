using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data.Repositories.LottoGame
{
    public class UserLottoBetsRepository : IUserLottoBetsRepository
    {

        private readonly DataContext context;
        private readonly IMapper mapper;
        public UserLottoBetsRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<IEnumerable<UserLottoBets>> userSendedBets(int userId)
        {

            var user = context.userLottoBets.Include(u => u.user)
                .Where(u => u.user.userId == userId)
                .ToListAsync();

            return await user;
        }

        public async void deleteSendedBets(UserLottoBets userLottoBets, int userId) {

            int count = await this.context.userLottoBets.Include(u => u.user).CountAsync();

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