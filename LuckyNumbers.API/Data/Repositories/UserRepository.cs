using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Entities;
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

        public UserRepository(DataContext con) : base(con)
        {
            context = con;
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

        public async Task<IEnumerable<User>> best5Players()
        {
            var users = context.users.Include(u => u.userExperience)
                .OrderByDescending(l => l.userExperience.level)
                .ThenByDescending(e => e.userExperience.experience).Take(5)
                .ToListAsync();

            return await users;
        }

        public int getLastId() {
            var users = context.users.OrderByDescending(u => u.userId).Take(1);

            return users.FirstOrDefault().userId;
        }
    }
}