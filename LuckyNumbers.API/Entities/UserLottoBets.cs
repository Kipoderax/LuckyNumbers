namespace LuckyNumbers.API.Entities
{
    public class UserLottoBets
    {
        public int lottoBetsId { get; set; }
        public int number1 { get; set; }
        public int number2 { get; set; }
        public int number3 { get; set; }
        public int number4 { get; set; }
        public int number5 { get; set; }
        public int number6 { get; set; }
        public User user { get; set; }
    }
}