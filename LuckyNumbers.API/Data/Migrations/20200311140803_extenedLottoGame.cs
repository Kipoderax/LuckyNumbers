using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyNumbers.API.Migrations
{
    public partial class extenedLottoGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "profit",
                table: "lottoGames",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profit",
                table: "lottoGames");
        }
    }
}
