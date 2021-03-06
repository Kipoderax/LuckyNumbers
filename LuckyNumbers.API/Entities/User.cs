using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LuckyNumbers.API.Entities
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string username { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string email { get; set; }
        public int saldo { get; set; }
        public DateTime lastLogin { get; set; }
        public DateTime created { get; set; }

        public ICollection<HistoryGameForLotto> lottoHistoryGames { get; set; }
        public ICollection<UserLottoBets> userlottoBets { get; set; }
        public LottoGame lottoGame { get; set; }
        public UserExperience userExperience { get; set; }
        public LatestDrawLottoNumbers latestDrawLottoNumbers { get; set; }
    }
}