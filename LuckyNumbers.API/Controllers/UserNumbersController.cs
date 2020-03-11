using System.Threading.Tasks;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace LuckyNumbers.API.Controllers
{
    [Route("/api/lotto")]
    [ApiController]
    public class UserNumbersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ILottoNumbersService lottoNumbersService;
        private readonly IResultUserLottoNumbers result;
        public UserNumbersController(IUserRepository userRepository,
                                     ILottoNumbersService lottoNumbersService,
                                     IResultUserLottoNumbers result)
        {
            this.userRepository = userRepository;
            this.lottoNumbersService = lottoNumbersService;
            this.result = result;
        }

        [HttpPost("{userId}/{amountBetsToSend}")]
        public async Task<IActionResult> saveUserGenerateNumbers(int userId, int amountBetsToSend)
        {
            
            lottoNumbersService.sendGenerateNumbers(userId, amountBetsToSend);

            return StatusCode(201);
        }

        

        [HttpPost("{userId}")]
        public async Task<IActionResult> saveUserInputNumbers(int userId, LottoNumbersDto lottoNumbersDto)
        {
            var userFromRepo = await userRepository.getUserByUserId(userId);
            int[] tabOfLottoNumbers = lottoNumbersService.tabOfLottoNumbersDto(lottoNumbersDto);
            lottoNumbersService.sortLottoNumbers(tabOfLottoNumbers);

            if (!lottoNumbersService.isUserHaveSaldo(userFromRepo.saldo))
            {
                return BadRequest("Brak salda na kolejny zakład");
            }
            lottoNumbersService.sendInputNumbers(userId, tabOfLottoNumbers);


            if (!lottoNumbersService.isNumberDuplicated(tabOfLottoNumbers))
            {
                return BadRequest("Liczby nie mogą się powtarzać");
            }

            if (!lottoNumbersService.isCorrectRange(lottoNumbersDto))
            {
                return BadRequest("Liczby muszą być w zakresie 1 - 49");
            }


            await userRepository.saveAll();

            return StatusCode(201);
        }

        [HttpPost("/api/lotto/result/{userId}")]
        public async Task<IActionResult> checkResult(int userId)
        {
            result.resultLottoGame(userId);

            await userRepository.saveAll();

            return StatusCode(201);
        }
    }
}