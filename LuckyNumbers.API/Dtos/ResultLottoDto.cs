using System.Collections.Generic;

namespace LuckyNumbers.API.Dtos
{
    public class ResultLottoDto
    {
        public int failGoal { get; set; }
        public int goal1Number { get; set; }
        public int goal2Numbers { get; set; }
        public int goal3Numbers { get; set; }
        public int goal4Numbers { get; set; }
        public int goal5Numbers { get; set; }
        public int goal6Numbers { get; set; }
        public int totalCostBets { get; set; }
        public int totalEarnMoney { get; set; }
        public int totalEarnExp { get; set; }
        public List<LottoNumbersDto> betsWithGoal3Numbers { get; set; } = new List<LottoNumbersDto>();
        public List<LottoNumbersDto> betsWithGoal4Numbers { get; set; } = new List<LottoNumbersDto>();
        public List<LottoNumbersDto> betsWithGoal5Numbers { get; set; } = new List<LottoNumbersDto>();
        public List<LottoNumbersDto> betsWithGoal6Numbers { get; set; } = new List<LottoNumbersDto>();
    }
}