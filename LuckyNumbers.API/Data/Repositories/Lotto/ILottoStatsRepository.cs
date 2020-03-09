using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data.Repositories.Lotto
{
    public interface ILottoStatsRepository
    {
         Task<List<int>> serverStatus();
         void updateUserGameStats(LottoGame lottoGame, User userRepo, ResultLottoDto resultLotto);
    }
}
