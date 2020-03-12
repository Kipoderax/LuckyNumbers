using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Data.Repositories.Lotto;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Service
{
    public class ResultUserLottoNumbers : IResultUserLottoNumbers
    {
        private readonly IUserExperienceRepository userExpRepo;
        private readonly IUserRepository userRepository;
        private readonly IHistoryGameRepository historyGameRepository;
        private readonly IUserLottoBetsRepository betsRepository;
        private readonly IMapper mapper;
        UserExperience userExperience;
        LottoGame lottoGame;
        HistoryGameForLotto historyGame;
        ILottoStatsRepository lottoStatsRepo;
        
        public ResultUserLottoNumbers(IUserExperienceRepository userExpRepo,
                                      IUserRepository userRepository, 
                                      IHistoryGameRepository historyGameRepository,
                                      IUserLottoBetsRepository betsRepository,
                                      ILottoStatsRepository lottoStatsRepo,
                                      IMapper mapper)
        {
            this.historyGameRepository = historyGameRepository;
            this.userRepository = userRepository;
            this.userExpRepo = userExpRepo;
            this.betsRepository = betsRepository;
            this.lottoStatsRepo = lottoStatsRepo;

            this.mapper = mapper;

            userExperience = new UserExperience();
            lottoGame = new LottoGame();
            historyGame = new HistoryGameForLotto();
        }

        public ResultLottoDto resultLottoGame(int userId)
        {
            ResultLottoDto result = new ResultLottoDto();
            ReadUrlPlainText lastDrawNumbers = new ReadUrlPlainText();
            List<LottoNumbersDto> userLottoBets = getUserBetsForCheck(userId);

            int[] lastDrawLottoNumbers = lastDrawNumbers.readRawLatestLottoNumbers();
            int[] numbersToCheck = new int[6];

            for (int k = 0; k < userLottoBets.Count; k++)
            {
                int goalNumber = 0;
                numbersToCheck[0] = userLottoBets[k].number1;
                numbersToCheck[1] = userLottoBets[k].number2;
                numbersToCheck[2] = userLottoBets[k].number3;
                numbersToCheck[3] = userLottoBets[k].number4;
                numbersToCheck[4] = userLottoBets[k].number5;
                numbersToCheck[5] = userLottoBets[k].number6;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (numbersToCheck[i] == lastDrawLottoNumbers[j])
                        {
                            goalNumber++;
                        }
                    }
                }

                countGoalNumbers(goalNumber, ref result);
                goalBetsWithSuccess(goalNumber, userLottoBets[k], result);
                result.totalEarnExp += addUserExperience(goalNumber);
                result.totalEarnMoney += addUserMoneyRewards(goalNumber);

            }

            result.totalCostBets = userLottoBets.Count * 3;

            updateUserStats(result, userId);
            userRepository.add(historyGame);
            userRepository.update(userExperience);
            userRepository.update(lottoGame);
            betsRepository.deleteSendedBets(userId);

            return result;
        }

        private void countGoalNumbers(int goal, ref ResultLottoDto resultDto)
        {

            switch (goal)
            {
                case 0:
                    resultDto.failGoal++;
                    break;
                case 1:
                    resultDto.goal1Number++;
                    break;
                case 2:
                    resultDto.goal2Numbers++;
                    break;
                case 3:
                    resultDto.goal3Numbers++;
                    break;
                case 4:
                    resultDto.goal4Numbers++;
                    break;
                case 5:
                    resultDto.goal5Numbers++;
                    break;
                case 6:
                    resultDto.goal6Numbers++;
                    break;
            }
        }

        private int addUserExperience(int goal)
        {
            int exp = 0;
            switch (goal)
            {

                case 1:
                    exp += (int)ExperiencePoints.ONEGOAL;
                    break;
                case 2:
                    exp += (int)ExperiencePoints.TWOGOALS;
                    break;
                case 3:
                    exp += (int)ExperiencePoints.THREEGOALS;
                    break;
                case 4:
                    exp += (int)ExperiencePoints.FOURGOALS;
                    break;
                case 5:
                    exp += (int)ExperiencePoints.FIVEGOALS;
                    break;
                case 6:
                    exp += (int)ExperiencePoints.SIXGOALS;
                    break;
            }

            return exp;
        }

        private int addUserMoneyRewards(int goals) {
            int moneyRewards = 0;
            ReadUrlPlainText reward = new ReadUrlPlainText();

            for(int i = 3; i <= 6; i++ ) {
                if (goals == i) {
                    moneyRewards += reward.readPriceForGoalLottoNumbers()[i-3];
                }
            }

            return moneyRewards;
        }

        private void goalBetsWithSuccess(int goals, LottoNumbersDto userLottoBets, ResultLottoDto resultLotto){
            switch (goals) {
                case 3:
                    resultLotto.betsWithGoal3Numbers.Add(userLottoBets);
                    break;
                case 4:
                    resultLotto.betsWithGoal4Numbers.Add(userLottoBets);
                    break;
                case 5:
                    resultLotto.betsWithGoal5Numbers.Add(userLottoBets);
                    break;
                case 6:
                    resultLotto.betsWithGoal6Numbers.Add(userLottoBets);
                    break;
            }
        }

        private List<LottoNumbersDto> getUserBetsForCheck(int userId) {
            List<LottoNumbersDto> userLottoBets = new List<LottoNumbersDto>();

            var userNumbers = betsRepository.userSendedBets(userId).Result;
            var numbersToReturn = mapper.Map<IEnumerable<LottoNumbersDto>>(userNumbers);
            userLottoBets = numbersToReturn.Cast<LottoNumbersDto>().ToList();

            return userLottoBets;
        }

        private async void updateUserStats(ResultLottoDto resultLotto, int userId)
        {
            Experience experience = new Experience();

            var userRepo = await userRepository.getUserByUserId(userId);
            int newExp = userExpRepo.getUserExperience(userId) + resultLotto.totalEarnExp;
            userRepo.saldo += 4 * experience.currentLevel(newExp) + resultLotto.totalEarnMoney;

            if(userRepo.userExperience.level > 5){
                lottoGame.maxBetsToSend = System.Math.Min(userRepo.saldo / 3, experience.currentLevel(newExp) * 2);
             } else lottoGame.maxBetsToSend = 10;

            userExpRepo.updateUserExperience(userId, newExp, userExperience);
            lottoStatsRepo.updateUserGameStats(lottoGame, userRepo, resultLotto);
            historyGameRepository.updateUserHistory(resultLotto, historyGame, userId);
        }
    }
}