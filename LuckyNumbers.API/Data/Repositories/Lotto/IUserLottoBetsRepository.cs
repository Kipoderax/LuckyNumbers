using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data.Repositories.Lotto
{
    public interface IUserLottoBetsRepository
    {
        Task<IEnumerable<UserLottoBets>> userSendedBets(int userId);
        void deleteSendedBets(int userId);
        int getLastBetId();
    }
}