using System.ComponentModel.DataAnnotations;

namespace LuckyNumbers.API.Entities
{
    public class UserLottoBets
    {
        [Key]
        public int lottoBetsId { get; set; }
        [Range(1, 49)]
        public int number1 { get; set; }
        [Range(1, 49)]
        public int number2 { get; set; }
        [Range(1, 49)]
        public int number3 { get; set; }
        [Range(1, 49)]
        public int number4 { get; set; }
        [Range(1, 49)]
        public int number5 { get; set; }
        [Range(1, 49)]
        public int number6 { get; set; }
        public User user { get; set; }
        public int userId { get; set; }
    }
}