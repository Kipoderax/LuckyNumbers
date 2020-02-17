namespace LuckyNumbers.API.Entities
{
    public class UserExperience
    {
        public int userExperienceId { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public User user { get; set; }
    }
}