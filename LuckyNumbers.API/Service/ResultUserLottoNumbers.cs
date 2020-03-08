using System.Collections.Generic;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Service
{
    public class ResultUserLottoNumbers : IResultUserLottoNumbers
    {
        private readonly IUserExperienceRepository userExpRepo;
        private readonly IUserRepository userRepository;
        public ResultUserLottoNumbers(IUserExperienceRepository userExpRepo, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.userExpRepo = userExpRepo;
        }

        public ResultLottoDto resultLottoGame(List<LottoNumbersDto> userLottoBets, int userId)
        {
            ResultLottoDto result = new ResultLottoDto();
            ReadUrlPlainText lastDrawNumbers = new ReadUrlPlainText();
            UserExperience userExperience = new UserExperience();
            Experience experience = new Experience();

            int[] lastDrawLottoNumbers = lastDrawNumbers.readRawLatestLottoNumbers();
            int[] numbersToCheck = new int[6];

            //user stats before result
            int userCurrentExp = userExpRepo.getUserExperience(userId);

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
                userCurrentExp += addUserExperience(goalNumber);
            }

            System.Console.WriteLine(userCurrentExp);
            userExperience.level = experience.currentLevel(userCurrentExp);
            userExperience.experience = userCurrentExp;
            userExperience.userId = userId;
            userRepository.add(userExperience);
            result.totalCostBets = userLottoBets.Count * 3;

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

        public int addUserExperience(int goal)
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

        private void updateHistoryLottoGame(ResultLottoDto resultLotto)
        {

        }
    }
}