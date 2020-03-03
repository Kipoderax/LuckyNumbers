using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using LuckyNumbers.API.Service;

namespace LuckyNumbers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public AuthController(IAuthRepository repository, IConfiguration config, IMapper mapper)
        {
            this.mapper = mapper;
            this.config = config;
            this.authRepository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDto userRegisterDto)
        {
            ReadUrlPlanText readUrlPlanText = new ReadUrlPlanText();
            userRegisterDto.username = userRegisterDto.username.ToLower();

            if (await authRepository.userExists(userRegisterDto.username))
            {
                return BadRequest("Nazwa użytkownika " + userRegisterDto.username + " zajęta");
            }

            var userToCreate = new User();
            var lottogame = new LottoGame();
            var userExp = new UserExperience();
            var latestDrawLottoNumbers = new LatestDrawLottoNumbers();

            int[] drawNumbers = readUrlPlanText.readRawLatestLottoNumbers();

            userToCreate.username = userRegisterDto.username;
            userToCreate.email = userRegisterDto.email;
            userToCreate.created = DateTime.Now;
            userToCreate.lastLogin = DateTime.Now;
            userToCreate.saldo = 30;

            lottogame.maxBetsToSend = 10;
            userExp.experience = 0;
            userExp.level = 1;

            latestDrawLottoNumbers.number1 = drawNumbers[0];
            latestDrawLottoNumbers.number2 = drawNumbers[1];
            latestDrawLottoNumbers.number3 = drawNumbers[2];
            latestDrawLottoNumbers.number4 = drawNumbers[3];
            latestDrawLottoNumbers.number5 = drawNumbers[4];
            latestDrawLottoNumbers.number6 = drawNumbers[5];

            userToCreate.lottoGame = lottogame;
            userToCreate.userExperience = userExp;
            userToCreate.latestDrawLottoNumbers = latestDrawLottoNumbers;

            var createdUser = await authRepository.register(userToCreate, userRegisterDto.password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDto userLoginDto)
        {
            var userFromRepo = await authRepository.login(userLoginDto.username.ToLower(), userLoginDto.password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.userId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

    }
}