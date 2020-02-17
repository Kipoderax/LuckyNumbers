namespace LuckyNumbers.API.Entities
{
    public class LottoGame
    {
        public int lottoGameId { get; set; }
        public int amountOfThree { get; set; }
        public int amountOfFour { get; set; }
        public int amountOfFive { get; set; }
        public int amountOfSix { get; set; }
        public int betsSended { get; set; }
        public User user { get; set; }
    }
}