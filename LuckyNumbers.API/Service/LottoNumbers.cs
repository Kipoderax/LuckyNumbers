using System;
using System.Collections.Generic;

namespace LuckyNumbers.API.Service
{
    public class LottoNumbers
    {
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