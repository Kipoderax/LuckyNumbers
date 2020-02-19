using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data
{
    public interface IUserRepository : IGenericRepository
    {
         Task<IEnumerable<User>> getUsers();
         Task<User> getUser(string username);
    }
}