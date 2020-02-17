using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data
{
    public interface IAuthRepository
    {
         Task<User> login(string username, string password);
         Task<User> register(User user, string password);
         Task<bool> userExists(string username);
    }
}