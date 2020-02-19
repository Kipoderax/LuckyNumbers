using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Dtos
{
    public class UserStatisticsDto
    {
        public string username { get; set; }
        public int betsSended { get; set; }
        public int amountOfThree { get; set; }
        public int amountOfFour { get; set; }
        public int amountOfFive { get; set; }
        public int amountOfSix { get; set; }
        public int level { get; set; }
        public int experience { get; set; }

    }
}