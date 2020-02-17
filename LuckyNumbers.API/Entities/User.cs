using System;
using System.Collections.Generic;

namespace LuckyNumbers.API.Entities
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string email { get; set; }
        public DateTime lastLogin { get; set; }
        public DateTime created { get; set; }

        public ICollection<HistoryGameForLotto> lottoHistoryGames { get; set; }
    }
}