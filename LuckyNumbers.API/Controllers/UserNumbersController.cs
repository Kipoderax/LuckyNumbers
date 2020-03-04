using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using LuckyNumbers.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace LuckyNumbers.API.Controllers
{
    [Route("/api/lotto")]
    [ApiController]
    public class UserNumbersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        ResultLottoDto resultDto;
        public UserNumbersController(IUserRepository userRepository,
        IMapper mapper)
        {
            resultDto = new ResultLottoDto();

            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost("{userId}/{amountBetsToSend}")]
        public async Task<IActionResult> saveUserGenerateNumbers(int userId, LottoNumbersDto lottoNumbersDto, int amountBetsToSend)
        {
            var userFromRepo = await userRepository.getUserByUserId(userId);

            UserLottoBets userLottoBets = new UserLottoBets();
            LottoNumbers lottoNumbers = new LottoNumbers();

            if (amountBetsToSend * 3 > userFromRepo.saldo)
            {
                return BadRequest("Ilość zakładów do wysłania przekracza możliwości salda");
            }

            for (int i = 0; i < amountBetsToSend; i++)
            {
                int[] numbers = lottoNumbers.generateNumbers();
                userLottoBets.number1 = numbers[0];
                userLottoBets.number2 = numbers[1];
                userLottoBets.number3 = numbers[2];
                userLottoBets.number4 = numbers[3];
                userLottoBets.number5 = numbers[4];
                userLottoBets.number6 = numbers[5];
                userLottoBets.userId = userId;

                var bet = mapper.Map(userLottoBets, lottoNumbersDto);

                userLottoBets.user = userFromRepo;
                userFromRepo.saldo -= 3;

                userRepository.add(userLottoBets);
            }
            await userRepository.saveAll();

            return StatusCode(201);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> saveUserInputNumbers(int userId, LottoNumbersDto lottoNumbersDto)
        {
            var userFromRepo = await userRepository.getUserByUserId(userId);

            if (userFromRepo.saldo < 3)
            {
                return BadRequest("Brak salda na kolejny zakład");
            }

            UserLottoBets userLottoBets = new UserLottoBets();
            LottoNumbers lottoNumbers = new LottoNumbers();

            int[] tabOfLottoNumbers = new int[] {
                lottoNumbersDto.number1, lottoNumbersDto.number2,
                lottoNumbersDto.number3, lottoNumbersDto.number4,
                lottoNumbersDto.number5, lottoNumbersDto.number6
            };
            lottoNumbers.sortLottoNumbers(tabOfLottoNumbers);

            userLottoBets.number1 = tabOfLottoNumbers[0];
            userLottoBets.number2 = tabOfLottoNumbers[1];
            userLottoBets.number3 = tabOfLottoNumbers[2];
            userLottoBets.number4 = tabOfLottoNumbers[3];
            userLottoBets.number5 = tabOfLottoNumbers[4];
            userLottoBets.number6 = tabOfLottoNumbers[5];
            userLottoBets.userId = userId;

            if (!lottoNumbers.isNumberDuplicated(tabOfLottoNumbers))
            {
                return BadRequest("Liczby nie mogą się powtarzać");
            }

            if (!lottoNumbers.isCorrectRange(tabOfLottoNumbers))
            {
                return BadRequest("Liczby muszą być w zakresie 1 - 49");
            }

            userFromRepo.saldo -= 3;

            var bet = mapper.Map(userLottoBets, lottoNumbersDto);

            userRepository.add(userLottoBets);

            await userRepository.saveAll();

            return StatusCode(201);
        }

        [HttpPost("/api/lotto/result/{userId}")]
        public async Task<string> checkResult(int userId)
        {
            List<LottoNumbersDto> userLottoBets = new List<LottoNumbersDto>();
            var userFromRepo = await userRepository.getUserByUserId(userId);

            var userNumbers = userRepository.userSendedBets(userId).Result;
            var numbersToReturn = mapper.Map<IEnumerable<LottoNumbersDto>>(userNumbers);
            userLottoBets = numbersToReturn.Cast<LottoNumbersDto>().ToList();

            ResultUserLottoNumbers result = new ResultUserLottoNumbers();
            resultDto = result.resultLottoGame(userLottoBets);

            var historyGame = new HistoryGameForLotto();
            var userLottoBetsEntity = new UserLottoBets();
            userLottoBetsEntity.userId = userId;

            historyGame.userId = userId;
            historyGame.betsSended = userLottoBets.Count;
            historyGame.amountGoalThrees = resultDto.goal3Numbers;
            historyGame.amountGoalFours = resultDto.goal4Numbers;
            historyGame.amountGoalFives = resultDto.goal5Numbers;
            historyGame.amountGoalSixes = resultDto.goal6Numbers;
            historyGame.user = userFromRepo;

            userRepository.add(historyGame);
            if(userLottoBets.Count > 0) {
                userRepository.deleteSendedBets(userLottoBetsEntity, userId);
            }

            await userRepository.saveAll();

            return "Pudeł: " + resultDto.failGoal.ToString() + 
                    "\nTrafienia 1 liczby: " + resultDto.goal1Number.ToString() +
                    "\nTrafienia 2 liczb: " + resultDto.goal2Numbers.ToString() +
                    "\nTrafienia 3 liczb: " + resultDto.goal3Numbers.ToString() +
                    "\nTrafienia 4 liczb: " + resultDto.goal4Numbers.ToString() +
                    "\nTrafienia 5 liczb: " + resultDto.goal5Numbers.ToString() +
                    "\nTrafienia 6 liczb: " + resultDto.goal6Numbers.ToString() + 
                    "\nNa " + userLottoBets.Count + " zakladow wydano " + resultDto.totalCostBets;
        }
    }
}