using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data.Repositories
{
    public class UserExperienceRepository : IUserExperienceRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public UserExperienceRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public int getUserExperience(int userId)
        {
            int exp = this.context.userExperiences.Include(u => u.user).Where(i => i.userId == userId).Select(e => e.experience).FirstOrDefault();
            return exp;
        }
    }
}