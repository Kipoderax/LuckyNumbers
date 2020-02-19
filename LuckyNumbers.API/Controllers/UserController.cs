using System.Threading.Tasks;
using LuckyNumbers.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace LuckyNumbers.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> getUsers() {
            var users = await repository.getUsers();

            return Ok(users);
        }
        
        [HttpGet("{username}")]
        public async Task<IActionResult> getUser(string username) {
            var user = await repository.getUser(username);

            return Ok(user);
        }
    }
}