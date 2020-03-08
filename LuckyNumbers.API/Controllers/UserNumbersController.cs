using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories;
using LuckyNumbers.API.Data.Repositories.LottoGame;
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
        private readonly IHistoryGameRepository historyGameRepostory;
        private readonly IUserLottoBetsRepository userLottoBetsRepository;
        private readonly ILottoNumbersService lottoNumbersService;
        private readonly IMapper mapper;
        ResultLottoDto resultDto;
        public UserNumbersController(IUserRepository userRepository,
                                     IHistoryGameRepository historyGameRepostory,
                                     IUserLottoBetsRepository userLottoBetsRepository,
                                     ILottoNumbersService lottoNumbersService,
                                     IMapper mapper)
        {
            this.userLottoBetsRepository = userLottoBetsRepository;
            this.historyGameRepostory = historyGameRepostory;
            resultDto = new ResultLottoDto();

            this.lottoNumbersService = lottoNumbersService;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

    [HttpPost("{userId}/{amountBetsToSend}")]
    public async Task<IActionResult> saveUserGenerateNumbers(int userId, LottoNumbersDto lottoNumbersDto, int amountBetsToSend)
    {
        var userFromRepo = await userRepository.getUserByUserId(userId);

        UserLottoBets userLottoBets = new UserLottoBets();
        LottoNumbersService lottoNumbers = new LottoNumbersService();

        if (!lottoNumbersService.isUserHaveSaldo(amountBetsToSend, userFromRepo.saldo)) {

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
    public async Task<string> checkResult(int userId)
    {
        List<LottoNumbersDto> userLottoBets = new List<LottoNumbersDto>();
        var userFromRepo = await userRepository.getUserByUserId(userId);
        var sendedBetss = await historyGameRepostory.userHistoryGame(userFromRepo.username);

        var userNumbers = userLottoBetsRepository.userSendedBets(userId).Result;
        var numbersToReturn = mapper.Map<IEnumerable<LottoNumbersDto>>(userNumbers);
        userLottoBets = numbersToReturn.Cast<LottoNumbersDto>().ToList();

        ResultUserLottoNumbers result = new ResultUserLottoNumbers();
        resultDto = result.resultLottoGame(userLottoBets);

        var historyGame = new HistoryGameForLotto();
        var lottoGame = new LottoGame();
        var userLottoBetsEntity = new UserLottoBets();
        userLottoBetsEntity.userId = userId;

        historyGame.userId = userId;
        historyGame.betsSended = userLottoBets.Count;
        historyGame.amountGoalThrees = resultDto.goal3Numbers;
        historyGame.amountGoalFours = resultDto.goal4Numbers;
        historyGame.amountGoalFives = resultDto.goal5Numbers;
        historyGame.amountGoalSixes = resultDto.goal6Numbers;
        historyGame.user = userFromRepo;

        lottoGame.userId = userId;
        lottoGame.betsSended = sendedBetss.Select(u => u.betsSended).Sum() + userLottoBets.Count;
        lottoGame.amountOfThree = sendedBetss.Select(u => u.amountGoalThrees).Sum() + resultDto.goal3Numbers;
        lottoGame.amountOfFour = sendedBetss.Select(u => u.amountGoalFours).Sum() + resultDto.goal4Numbers;
        lottoGame.amountOfFive = sendedBetss.Select(u => u.amountGoalFives).Sum() + resultDto.goal5Numbers;
        lottoGame.amountOfSix = sendedBetss.Select(u => u.amountGoalSixes).Sum() + resultDto.goal6Numbers;
        lottoGame.maxBetsToSend = 10;
        lottoGame.user = userFromRepo;

        userFromRepo.saldo = 30;

        userRepository.add(historyGame);
        userRepository.update(lottoGame);
        
        userLottoBetsRepository.deleteSendedBets(userLottoBetsEntity, userId);

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