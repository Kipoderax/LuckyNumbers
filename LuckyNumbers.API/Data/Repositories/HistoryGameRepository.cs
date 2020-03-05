using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data
{
    public class HistoryGameRepository : IHistoryGameRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public HistoryGameRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<HistoryGameForLotto>> userHistoryGame(string username)
        {
            var user = context.lottoHistoryGames.Include(u => u.user)
                .Where(u => u.user.username == username).OrderByDescending(u => u.historyLottoId)
                .ToListAsync();

            return await user;
        }

        public async Task<IEnumerable<HistoryGameForLotto>> top5Xp()
        {
            var user = context.lottoHistoryGames.Include(u => u.user)
                .OrderByDescending(x => x.experience).Take(5)
                .ToListAsync();

            return await user;
        }
    }
}