﻿using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Data.Repositories.Lotto;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Service;
using Microsoft.AspNetCore.Mvc;
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

        public ServerController(IUserRepository userRepository,
                                  IHistoryGameRepository historyGameRepository,
                                  ILottoStatsRepository lottoGameRepository,
                                  IMapper mapper)
        {
            this.lottoGameRepository = lottoGameRepository;
            this.historyGameRepository = historyGameRepository;

            this.userRepository = userRepository;
            this.mapper = mapper;
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

        [HttpGet("/api/latest-numbers")]
        public int[] getLatestDrawNumbers()
        {
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
