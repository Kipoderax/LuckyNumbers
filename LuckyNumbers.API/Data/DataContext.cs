using LuckyNumbers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyNumbers.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        public DbSet<User> users { get; set; }
        public DbSet<HistoryGameForLotto> lottoHistoryGames { get; set; }
        public DbSet<LottoGame> lottoGames { get; set; }
        public DbSet<UserExperience> userExperiences { get; set; }
        public DbSet<UserLottoBets> userLottoBets { get; set; }
    }
}