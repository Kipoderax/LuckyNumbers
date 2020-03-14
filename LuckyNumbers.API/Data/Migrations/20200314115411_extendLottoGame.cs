using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyNumbers.API.Migrations
{
    public partial class extendLottoGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "resultCheck",
                table: "lottoGames",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "resultCheck",
                table: "lottoGames");
        }
    }
}
