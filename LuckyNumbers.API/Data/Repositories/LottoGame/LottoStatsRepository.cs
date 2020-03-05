using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data.Repositories
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
    }
}