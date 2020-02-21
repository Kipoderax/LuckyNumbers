using LuckyNumbers.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyNumbers.API.Controllers {

    [Route( "api/server" )]
    [ApiController]
    public class ServerController {

        private readonly IUserRepository userRepository;

        public ServerController ( IUserRepository userRepository) {

            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<int>> getResults () {
            return await userRepository.getBetsSended();
        }

    }
}
