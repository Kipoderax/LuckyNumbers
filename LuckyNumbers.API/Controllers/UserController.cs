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
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        public UserController(IUserRepository repository,
                              IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

    [HttpGet]
    public async Task<IActionResult> getUsers()
    {
        var users = await repository.getUsers();

        var usersToReturn = mapper.Map<IEnumerable<UserStatisticsDto>>(users);

        return Ok(usersToReturn);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> getUser(string username)
    {
        var user = await repository.getUser(username);

        var userToReturn = mapper.Map<UserDetailsDto>(user);

        return Ok(userToReturn);
    }
}
}