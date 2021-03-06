using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data
{
    public interface IUserRepository : IGenericRepository
    {
        Task<IEnumerable<User>> getUsers();
        Task<User> getUserByUsername(string username);
        Task<User> getUserByUserId(int userId);
        Task<IEnumerable<User>> best5Players ();
        int getLastId();
    }
}