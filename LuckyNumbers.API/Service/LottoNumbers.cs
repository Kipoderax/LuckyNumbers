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
    }
}