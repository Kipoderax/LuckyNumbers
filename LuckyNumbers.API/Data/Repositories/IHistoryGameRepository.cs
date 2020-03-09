using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data.Repositories
{
    public interface IHistoryGameRepository
    {
         Task<IEnumerable<HistoryGameForLotto>> userHistoryGame(string username);
         Task<IEnumerable<HistoryGameForLotto>> top5Xp ();
         void updateUserHistory(ResultLottoDto result, HistoryGameForLotto historyGame, int userId);
    }
}