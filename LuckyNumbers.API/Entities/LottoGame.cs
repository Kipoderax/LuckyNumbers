using System.ComponentModel.DataAnnotations;

namespace LuckyNumbers.API.Entities
{
    public class LottoGame
    {
        [Key]
        public int lottoGameId { get; set; }
        public int amountOfThree { get; set; }
        public int amountOfFour { get; set; }
        public int amountOfFive { get; set; }
        public int amountOfSix { get; set; }
        public int betsSended { get; set; }
        public int maxBetsToSend { get; set; }
        public int profit { get; set; }
        public int resultCheck { get; set; }
        public User user { get; set; }
        public int userId { get; set; }
    }
}