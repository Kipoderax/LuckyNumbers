using System;

namespace LuckyNumbers.API.Dtos
{
    public class UserDetailsDto
    {
        public string username { get; set; }
        public DateTime lastLogin { get; set; }
        public DateTime created { get; set; }
        public string email { get; set; }
        public int saldo { get; set; }
        public int amountOfThree { get; set; }
        public int amountOfFour { get; set; }
        public int amountOfFive { get; set; }
        public int amountOfSix { get; set; }
        public int betsSended { get; set; }
        public int maxBetsToSend { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
    }
}