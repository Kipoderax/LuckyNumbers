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
            userRegisterDto.username = userRegisterDto.username.ToLower();

            if (await authRepository.userExists(userRegisterDto.username))
            {
                return BadRequest("Nazwa użytkownika " + userRegisterDto.username + " zajęta");
            }

            var userToCreate = new User {
                username = userRegisterDto.username,
                email = userRegisterDto.email,
                created = DateTime.Now,
                lastLogin = DateTime.Now,
                saldo = 30
            };

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