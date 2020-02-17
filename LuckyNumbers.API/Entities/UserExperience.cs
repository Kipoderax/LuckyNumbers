using System.ComponentModel.DataAnnotations;

namespace LuckyNumbers.API.Entities
{
    public class UserExperience
    {
        [Key]
        public int userExperienceId { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public User user { get; set; }
        public int userId { get; set; }
    }
}