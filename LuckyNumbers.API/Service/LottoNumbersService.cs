using System;
using System.Collections.Generic;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Service
{
    public class LottoNumbersService : ILottoNumbersService
    {
        public bool isUserHaveSaldo(int amountBets, int saldo) {
            if (amountBets * 3 > saldo) {
                return false;
            }

            return true;
        }

        public bool isUserHaveSaldo(int saldo) {
            if (saldo < 3) {
                return false;
            }

            return true;
        }

        public int[] tabOfLottoNumbersDto(LottoNumbersDto lottoNumbersDto) {
            int[] tabOfLottoNumbers = new int[] {
                lottoNumbersDto.number1, lottoNumbersDto.number2,
                lottoNumbersDto.number3, lottoNumbersDto.number4,
                lottoNumbersDto.number5, lottoNumbersDto.number6
            };

            return tabOfLottoNumbers;
        }
        public UserLottoBets inputNumbers(LottoNumbersDto lottoNumbersDto, int userId) {
            UserLottoBets userLottoBets = new UserLottoBets();

            int[] tabOfLottoNumbers = tabOfLottoNumbersDto(lottoNumbersDto);

            userLottoBets.number1 = tabOfLottoNumbers[0];
            userLottoBets.number2 = tabOfLottoNumbers[1];
            userLottoBets.number3 = tabOfLottoNumbers[2];
            userLottoBets.number4 = tabOfLottoNumbers[3];
            userLottoBets.number5 = tabOfLottoNumbers[4];
            userLottoBets.number6 = tabOfLottoNumbers[5];
            userLottoBets.userId = userId;

            return userLottoBets;
        }

        public void mapNumbersToUserLottoBets(ref UserLottoBets userLottoBets, int[] numbers) {
            userLottoBets.number1 = numbers[0];
            userLottoBets.number2 = numbers[1];
            userLottoBets.number3 = numbers[2];
            userLottoBets.number4 = numbers[3];
            userLottoBets.number5 = numbers[4];
            userLottoBets.number6 = numbers[5];
        }

        public int[] generateNumbers() {
            SortedSet<int> sortedLottoNumbers = new SortedSet<int>();
            Random randomNumber = new Random();
            int[] lottoNumbers = new int[6];

            while (sortedLottoNumbers.Count < 6)
            {
                sortedLottoNumbers.Add(randomNumber.Next(1, 49));
            }

            sortedLottoNumbers.CopyTo(lottoNumbers, 0, 6);

            return lottoNumbers;
        }

        public bool isNumberDuplicated(int[] lottoNumbersDto) {

            for (int i = 0; i < lottoNumbersDto.Length - 1; i++) {
                if (lottoNumbersDto[i] == lottoNumbersDto[i+1]) {
                    return false;
                }
            }

            return true;
        }

        public bool isCorrectRange(int[] lottoNumbers) {

            for (int i = 0; i < lottoNumbers.Length; i++) {
                if (lottoNumbers[i] < 1 || lottoNumbers[i] > 49) {
                    return false;
                }
            }

            return true;
        }

        public void sortLottoNumbers( int[] lottoNumbers ) {

            int max = findMax(lottoNumbers);
            int[] tempTab = new int[max + 1];
            
            for(int i = 0; i < lottoNumbers.Length; i++ ) {
                tempTab[lottoNumbers[i]]++;
            }

            int j = 0, k = 0;
            while(j < max + 1) {

                if(tempTab[j] > 0) {
                    lottoNumbers[k++] = j;
                    tempTab[j]--;
                } else j++;
            }
        }

        private int findMax(int[] tab) {
            int max = Int32.MinValue;
            for(int i = 0; i < tab.Length; i++) {
                if(tab[i] > max) {
                    max = tab[i];
                }
            }

            return max;
        }
    }
}