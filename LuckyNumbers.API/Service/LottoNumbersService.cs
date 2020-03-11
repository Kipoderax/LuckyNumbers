using System;
using System.Collections.Generic;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories.Lotto;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Service
{
    public class LottoNumbersService : ILottoNumbersService
    {
        private readonly IUserLottoBetsRepository betsRepository;
        private readonly IUserRepository userRepository;

        public LottoNumbersService() {}
        public LottoNumbersService(IUserLottoBetsRepository betsRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.betsRepository = betsRepository;
        }

        public async void sendGenerateNumbers(int userId, int amountBetsToSend)
        {
            var userFromRepo = await userRepository.getUserByUserId(userId);
            var userLottoBets = new UserLottoBets();

            userFromRepo.saldo -= 3 * amountBetsToSend;
            int lottoBetId = betsRepository.getLastBetId();
            for (int i = 0; i < amountBetsToSend; i++)
            {
                int[] numbers = generateNumbers();
                mapNumbersToUserLottoBets(ref userLottoBets, numbers);
                userLottoBets.userId = userId;
                userLottoBets.lottoBetsId = lottoBetId + i + 1;

                userRepository.add(userLottoBets);
                await userRepository.saveAll();
            }
        }

        public async void sendInputNumbers(int userId, int[] lottoNumbersDto) {
            var userLottoBets = new UserLottoBets();
            var userFromRepo = await userRepository.getUserByUserId(userId);

            userLottoBets.number1 = lottoNumbersDto[0];
            userLottoBets.number2 = lottoNumbersDto[1];
            userLottoBets.number3 = lottoNumbersDto[2];
            userLottoBets.number4 = lottoNumbersDto[3];
            userLottoBets.number5 = lottoNumbersDto[4];
            userLottoBets.number6 = lottoNumbersDto[5];
            userLottoBets.userId = userId;

            userFromRepo.saldo -= 3;

            userRepository.add(userLottoBets);
        }

        public bool isUserHaveSaldo(int saldo)
        {
            if (saldo < 3)
            {
                return false;
            }

            return true;
        }

        public int[] tabOfLottoNumbersDto(LottoNumbersDto lottoNumbersDto)
        {
            int[] tabOfLottoNumbers = new int[] {
                lottoNumbersDto.number1, lottoNumbersDto.number2,
                lottoNumbersDto.number3, lottoNumbersDto.number4,
                lottoNumbersDto.number5, lottoNumbersDto.number6
            };

            return tabOfLottoNumbers;
        }

        public void mapNumbersToUserLottoBets(ref UserLottoBets userLottoBets, int[] numbers)
        {
            userLottoBets.number1 = numbers[0];
            userLottoBets.number2 = numbers[1];
            userLottoBets.number3 = numbers[2];
            userLottoBets.number4 = numbers[3];
            userLottoBets.number5 = numbers[4];
            userLottoBets.number6 = numbers[5];
        }

        private int[] generateNumbers()
        {
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

        public bool isNumberDuplicated(int[] tabOfLottoNumbers)
        {
            for (int i = 0; i < tabOfLottoNumbers.Length - 1; i++)
            {
                if (tabOfLottoNumbers[i] == tabOfLottoNumbers[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        public bool isCorrectRange(LottoNumbersDto lottoNumbersDto)
        {
            int[] lottoNumbers = tabOfLottoNumbersDto(lottoNumbersDto);

            for (int i = 0; i < lottoNumbers.Length; i++)
            {
                if (lottoNumbers[i] < 1 || lottoNumbers[i] > 49)
                {
                    return false;
                }
            }

            return true;
        }

        public void sortLottoNumbers(int[] lottoNumbers)
        {
            
            int max = findMax(lottoNumbers);
            int[] tempTab = new int[max + 1];

            for (int i = 0; i < lottoNumbers.Length; i++)
            {
                tempTab[lottoNumbers[i]]++;
            }

            int j = 0, k = 0;
            while (j < max + 1)
            {

                if (tempTab[j] > 0)
                {
                    lottoNumbers[k++] = j;
                    tempTab[j]--;
                }
                else j++;
            }
        }

        private int findMax(int[] tab)
        {
            int max = Int32.MinValue;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] > max)
                {
                    max = tab[i];
                }
            }

            return max;
        }
    }
}