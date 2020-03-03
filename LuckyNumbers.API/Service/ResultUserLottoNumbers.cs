using System.Collections.Generic;
using LuckyNumbers.API.Dtos;

namespace LuckyNumbers.API.Service
{
    public class ResultUserLottoNumbers
    {

        public ResultLottoDto resultLottoGame(List<LottoNumbersDto> userLottoBets )
        {
            ResultLottoDto result = new ResultLottoDto();
            ReadUrlPlanText lastDrawNumbers = new ReadUrlPlanText();
            
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
            }
            result.totalCostBets = userLottoBets.Count * 3;

            return result;
        }

        private void countGoalNumbers(int goal, ref ResultLottoDto goalNumbers) {
            switch (goal) {
                case 0: 
                    goalNumbers.failGoal++;
                    break;
                case 1:
                    goalNumbers.goal1Number++;
                    break;
                case 2:
                    goalNumbers.goal2Numbers++;
                    break;
                case 3:
                    goalNumbers.goal3Numbers++;
                    break;
                case 4:
                    goalNumbers.goal4Numbers++;
                    break;
                case 5:
                    goalNumbers.goal5Numbers++;
                    break;
                case 6:
                    goalNumbers.goal6Numbers++;
                    break;
            }
        }
    }
}