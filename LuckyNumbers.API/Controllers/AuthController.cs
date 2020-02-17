using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;
        public AuthController(IAuthRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDto userRegisterDto) {
            userRegisterDto.username = userRegisterDto.username.ToLower();

            if (await repository.userExists(userRegisterDto.username))
            {
                return BadRequest("Nazwa użytkownika zajęta");
            }

            var userToCreate = new User {
                username = userRegisterDto.username
            };

            var createdUser = await repository.register(userToCreate, userRegisterDto.password);

            return StatusCode(201);
        }

    }
}