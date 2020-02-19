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

        public async Task<User> GetUser(string username)
        {
            var user = await context.users.Include(l => l.userlottoBets).FirstOrDefaultAsync(u => u.username == username);
            return user;
        }

        public async Task<IEnumerable<User>> getUsers()
        {
            var users = await context.users.Include(l => l.userlottoBets).ToListAsync();
            return users;
        }
    }
}