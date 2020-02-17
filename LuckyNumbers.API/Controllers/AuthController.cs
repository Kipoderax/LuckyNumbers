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

namespace LuckyNumbers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            this.config = config;
            this.repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.username = userRegisterDto.username.ToLower();

            if (await repository.userExists(userRegisterDto.username))
            {
                return BadRequest("Nazwa użytkownika " + userRegisterDto.username + " zajęta");
            }

            var userToCreate = new User
            {
                username = userRegisterDto.username
            };

            var createdUser = await repository.register(userToCreate, userRegisterDto.password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDto userLoginDto)
        {
            var userFromRepo = await repository.login(userLoginDto.username.ToLower(), userLoginDto.password);

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

            var tokenDescriptor = new SecurityTokenDescriptor {
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