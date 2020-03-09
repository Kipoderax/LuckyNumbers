using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Data.Repositories
{
    public interface IUserExperienceRepository
    {
         int getUserExperience(int userId);
         void updateUserExperience(int userId, int experience, UserExperience userExperience);
    }
}