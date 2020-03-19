using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Data.Repositories.Lotto;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Service;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyNumbers.API.Controllers
{

    [Route("/api/server")]
    [ApiController]
    public class ServerController : ControllerBase
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IHistoryGameRepository historyGameRepository;
        private readonly ILottoStatsRepository lottoGameRepository;
        private readonly IScheduler scheduler;

        public ServerController(IUserRepository userRepository,
                                  IHistoryGameRepository historyGameRepository,
                                  ILottoStatsRepository lottoGameRepository,
                                  IMapper mapper,
                                  IScheduler scheduler)
        {
            this.lottoGameRepository = lottoGameRepository;
            this.historyGameRepository = historyGameRepository;

            this.userRepository = userRepository;
            this.mapper = mapper;
            this.scheduler = scheduler;
        }

        [Route("/api/status")]
        [HttpGet]
        public async Task<List<int>> getServerStatus()
        {
            return await lottoGameRepository.serverStatus();
        }

        [HttpGet]
        public async Task<IActionResult> getBest5Players()
        {
            var users = await userRepository.best5Players();

            var usersToReturn = mapper.Map<IEnumerable<UserStatisticsDto>>(users);

            return Ok(usersToReturn);
        }

        [Route("/api/xp")]
        [HttpGet]
        public async Task<IActionResult> getXp()
        {
            var users = await historyGameRepository.top5Xp();

            var usersToReturn = mapper.Map<IEnumerable<HistoryGameDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("/api/history/{username}")]
        public async Task<IActionResult> getHistoryGame(string username)
        {
            var users = await historyGameRepository.userHistoryGame(username);

            var usersToReturn = mapper.Map<IEnumerable<HistoryGameDto>>(users);

            return Ok(usersToReturn);
        }

        //"0 40 22 ? * 3,5,7"
        [HttpGet("/api/latest-numbers")]
        public async Task<int[]> getLatestDrawNumbers()
        {
            // ITrigger trigger = TriggerBuilder.Create()
            //  .WithCronSchedule("0/5 * * ? * *") <- every 5 sec
            //  .WithPriority(1)
            //  .Build();

            // IJobDetail job = JobBuilder.Create<CheckDate>()
            //             .Build(); 

            // await scheduler.ScheduleJob(job, trigger);

            LottoNumbersService lottoNumbersService = new LottoNumbersService();
            ReadUrlPlainText rupt = new ReadUrlPlainText();
            int[] latestDrawNumber = rupt.readRawLatestLottoNumbers();
            rupt.readPriceForGoalLottoNumbers();
            
            lottoNumbersService.sortLottoNumbers(latestDrawNumber);
            return latestDrawNumber;
        }

        [HttpGet("/api/money-rewards")]
        public int[] getLatestMoneyRewards() {

            ReadUrlPlainText rewards = new ReadUrlPlainText();
            int[] latestRewards = rewards.readPriceForGoalLottoNumbers();

            return latestRewards;
        }
    }
}
