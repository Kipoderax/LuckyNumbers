namespace LuckyNumbers.API.Entities
{
    public class HistoryGameForLotto
    {
        public int historyLottoId { get; set; }
        public string dateGame { get; set; }
        public int betsSended { get; set; }
        public int amountGoalThrees { get; set; }
        public int amountGoalFours { get; set; }
        public int amountGoalFives { get; set; }
        public int amountGoalSixes { get; set; }
        public int experience { get; set; }
        public int result { get; set; }
        public User user { get; set; }
    }
}