using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data.Repositories.LottoGame
{
    public interface IUserLottoBetsRepository
    {
        Task<IEnumerable<UserLottoBets>> userSendedBets(int userId);
        void deleteSendedBets(UserLottoBets userLottoBets, int userId);
        int getLastBetId();
    }
}