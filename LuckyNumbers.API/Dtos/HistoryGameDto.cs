using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyNumbers.API.Dtos {
    public class HistoryGameDto {
        public string username { get; set; }
        public string dateGame { get; set; }
        public int amountBets { get; set; }
        public int amountGoalThrees { get; set; }
        public int amountGoalFours { get; set; }
        public int amountGoalFives { get; set; }
        public int amountGoalSixes { get; set; }
        public int experience { get; set; }
        public int result { get; set; }
    }
}
