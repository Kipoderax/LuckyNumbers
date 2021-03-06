using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Data.Repositories.Lotto;
using LuckyNumbers.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LuckyNumbers.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthRepository authRepository;
        private readonly IUserLottoBetsRepository userLottoBetsRepository;
        private readonly IMapper mapper;
        public UserController(IUserRepository userRepository,
                              IAuthRepository authRepository,
                              IUserLottoBetsRepository userLottoBetsRepository,
                              IMapper mapper)
        {
            this.userLottoBetsRepository = userLottoBetsRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.authRepository = authRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getUsers()
        {
            var users = await userRepository.getUsers();

            var usersToReturn = mapper.Map<IEnumerable<UserStatisticsDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> getUser(string username)
        {
            var user = await userRepository.getUserByUsername(username);

            if (!await authRepository.userExists(username))
            {
                return BadRequest("Nie ma takiego gracza");
            }

            var userToReturn = mapper.Map<UserDetailsDto>(user);

            return Ok(userToReturn);
        }

        [HttpGet("/api/sended-bets/{userId}")]
        public async Task<IActionResult> getUserSendedBets(int userId)
        {

            var user = await userLottoBetsRepository.userSendedBets(userId);
            var userToReturn = mapper.Map<IEnumerable<LottoNumbersDto>>(user);

            return Ok(userToReturn);
        }
    }
}