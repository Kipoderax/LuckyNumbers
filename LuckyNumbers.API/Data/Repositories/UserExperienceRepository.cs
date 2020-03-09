using System.Linq;
using AutoMapper;
using LuckyNumbers.API.Entities;
using LuckyNumbers.API.Service;
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
            int exp = this.context.userExperiences.Include(u => u.user).
                                Where(i => i.userId == userId).
                                Select(e => e.experience).
                                FirstOrDefault();
            return exp;
        }

        public void updateUserExperience(int userId, int experience, UserExperience userExperience) {
            Experience exp = new Experience();

            userExperience.userId = userId;
            userExperience.experience = experience;
            userExperience.level = exp.currentLevel(experience);
        }
    }
}