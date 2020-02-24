using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LuckyNumbers.API.Data;
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
        private readonly IMapper mapper;
        public UserController(IUserRepository userRepository,
                              IAuthRepository authRepository,
                              IMapper mapper)
        {
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

        [HttpGet("/api/sended-bets/{username}")]
        public async Task<IActionResult> getUserSendedBets(string username) {

            var user = await userRepository.userSendedBets(username);
            var userToReturn = mapper.Map<IEnumerable<LottoNumbersDto>>(user);

            return Ok(userToReturn);
        }
    }
}