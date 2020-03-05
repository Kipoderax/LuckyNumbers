using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data.Repositories
{
    public interface IHistoryGameRepository
    {
         Task<IEnumerable<HistoryGameForLotto>> userHistoryGame(string username);
         Task<IEnumerable<HistoryGameForLotto>> top5Xp ();
    }
}