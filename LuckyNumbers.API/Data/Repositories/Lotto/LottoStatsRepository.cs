using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data.Repositories.Lotto
{
    public class LottoStatsRepository : ILottoStatsRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public LottoStatsRepository(DataContext context, IMapper mapper) 
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<int>> serverStatus()
        {
            List<int> status = new List<int>();

            var registeredUsers = await context.users.CountAsync();
            status.Add(registeredUsers);

            var betsSended = await context.lottoGames.SumAsync(b => b.betsSended);
            status.Add(betsSended);

            var amountOfThree = await context.lottoGames.SumAsync(b => b.amountOfThree);
            status.Add(amountOfThree);

            var amountOfFour = await context.lottoGames.SumAsync(b => b.amountOfFour);
            status.Add(amountOfFour);

            var amountOfFive = await context.lottoGames.SumAsync(b => b.amountOfFive);
            status.Add(amountOfFive);

            var amountOfSix = await context.lottoGames.SumAsync(b => b.amountOfSix);
            status.Add(amountOfSix);

            return status;
        }

        public void updateUserGameStats(LottoGame lottoGame, User userRepo, ResultLottoDto resultLotto) {

             lottoGame.userId = userRepo.userId;
             lottoGame.betsSended = userRepo.lottoGame.betsSended + resultLotto.totalCostBets / 3;
             lottoGame.amountOfThree = userRepo.lottoGame.amountOfThree + resultLotto.goal3Numbers;
             lottoGame.amountOfFour = userRepo.lottoGame.amountOfFour + resultLotto.goal4Numbers;
             lottoGame.amountOfFive = userRepo.lottoGame.amountOfFive + resultLotto.goal5Numbers;
             lottoGame.amountOfSix = userRepo.lottoGame.amountOfSix + resultLotto.goal6Numbers;
             lottoGame.profit = userRepo.lottoGame.profit + resultLotto.totalEarnMoney - resultLotto.totalCostBets;
         }
    }
}
