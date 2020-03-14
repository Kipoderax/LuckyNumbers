using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using LuckyNumbers.API.Service;
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

        public void updateUserHistory(ResultLottoDto result, HistoryGameForLotto historyGame, int userId) {
            ReadUrlPlainText dataGame = new ReadUrlPlainText();

            historyGame.userId = userId;
            historyGame.dateGame = dataGame.readlatestDataLottoGame();
            historyGame.betsSended = result.totalCostBets / 3;
            historyGame.amountGoalThrees = result.goal3Numbers;
            historyGame.amountGoalFours = result.goal4Numbers;
            historyGame.amountGoalFives = result.goal5Numbers;
            historyGame.amountGoalSixes = result.goal6Numbers;
            historyGame.experience = (int) result.totalEarnExp;
            historyGame.result = result.totalEarnMoney - result.totalCostBets;
        }
    }
}