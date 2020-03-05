using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyNumbers.API.Data.Repositories
{
    public interface ILottoStatsRepository
    {
         Task<List<int>> serverStatus();
    }
}