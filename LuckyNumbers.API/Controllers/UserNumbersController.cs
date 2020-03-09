using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Data.Repositories.Lotto;
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
        private readonly IUserLottoBetsRepository userLottoBetsRepository;
        private readonly IHistoryGameRepository historyRepository;
        private readonly ILottoNumbersService lottoNumbersService;
        private readonly IResultUserLottoNumbers result;
        private readonly IMapper mapper;
        ResultLottoDto resultDto;
        public UserNumbersController(IUserRepository userRepository,
                                     IUserLottoBetsRepository userLottoBetsRepository,
                                     IHistoryGameRepository gameRepository,
                                     ILottoNumbersService lottoNumbersService,
                                     IResultUserLottoNumbers result,
                                     IMapper mapper)
        {
            this.userLottoBetsRepository = userLottoBetsRepository;
            this.userRepository = userRepository;
            this.historyRepository = gameRepository;
            resultDto = new ResultLottoDto();

            this.lottoNumbersService = lottoNumbersService;
            this.result = result;
            this.mapper = mapper;
        }

        [HttpPost("{userId}/{amountBetsToSend}")]
        public async Task<IActionResult> saveUserGenerateNumbers(int userId, LottoNumbersDto lottoNumbersDto, int amountBetsToSend)
        {
            var userFromRepo = await userRepository.getUserByUserId(userId);

            UserLottoBets userLottoBets = new UserLottoBets();
            LottoNumbersService lottoNumbers = new LottoNumbersService();

            if (!lottoNumbersService.isUserHaveSaldo(amountBetsToSend, userFromRepo.saldo))
            {

                return BadRequest("Ilość zakładów do wysłania przekracza możliwości salda");
            }

            userFromRepo.saldo -= 3 * amountBetsToSend;
            int lottoBetId = userLottoBetsRepository.getLastBetId();
            for (int i = 0; i < amountBetsToSend; i++)
            {
                int[] numbers = lottoNumbers.generateNumbers();
                lottoNumbers.mapNumbersToUserLottoBets(ref userLottoBets, numbers);
                userLottoBets.userId = userId;
                userLottoBets.user = userFromRepo;
                userLottoBets.lottoBetsId = lottoBetId + i + 1;

                var bet = mapper.Map(userLottoBets, lottoNumbersDto);

                userRepository.add(userLottoBets);
                await userRepository.saveAll();
            }

            return StatusCode(201);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> saveUserInputNumbers(int userId, LottoNumbersDto lottoNumbersDto)
        {
            var userLottoBets = new UserLottoBets();
            var userFromRepo = await userRepository.getUserByUserId(userId);

            if (!lottoNumbersService.isUserHaveSaldo(userFromRepo.saldo))
            {
                return BadRequest("Brak salda na kolejny zakład");
            }

            userLottoBets = lottoNumbersService.inputNumbers(lottoNumbersDto, userId);

            int[] tabOfLottoNumbers = lottoNumbersService.tabOfLottoNumbersDto(lottoNumbersDto);
            lottoNumbersService.sortLottoNumbers(tabOfLottoNumbers);

            if (!lottoNumbersService.isNumberDuplicated(tabOfLottoNumbers))
            {
                return BadRequest("Liczby nie mogą się powtarzać");
            }

            if (!lottoNumbersService.isCorrectRange(tabOfLottoNumbers))
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
        public async Task<IActionResult> checkResult(int userId)
        {
            resultDto = result.resultLottoGame(userId);

            await userRepository.saveAll();

            return StatusCode(201);
        }
    }
}