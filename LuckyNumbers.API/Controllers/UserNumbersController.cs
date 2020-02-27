using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using LuckyNumbers.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuckyNumbers.API.Controllers
{
    [Route("/api/lotto")]
    [ApiController]
    public class UserNumbersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserNumbersController(IUserRepository userRepository,
                                     IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        // [Authorize]
        [HttpPost("{userId}/{amountBetsToSend}")]
        public async Task<IActionResult> saveUserNumbers(int userId, LottoNumbersDto lottoNumbersDto, int amountBetsToSend) {
            var user = userRepository.getUserByUserId(userId);

            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            // {
            //     return Unauthorized();
            // }

            UserLottoBets userLottoBets = new UserLottoBets();
            LottoNumbers lottoNumbers = new LottoNumbers();

            for (int i = 0; i < amountBetsToSend; i++) {
                int[] numbers = lottoNumbers.generateNumbers();
                userLottoBets.number1 = numbers[0];
                userLottoBets.number2 = numbers[1];
                userLottoBets.number3 = numbers[2];
                userLottoBets.number4 = numbers[3];
                userLottoBets.number5 = numbers[4];
                userLottoBets.number6 = numbers[5];
                userLottoBets.userId = userId;

                var bet = mapper.Map(userLottoBets, lottoNumbersDto);

                userRepository.add(userLottoBets);
            }
                await userRepository.saveAll();

            return StatusCode(201);
        }
    }
}