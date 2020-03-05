using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyNumbers.API.Migrations
{
    public partial class latestDrawLottoNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "latestDrawLottoNumbers",
                columns: table => new
                {
                    latestLottoGameId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    number1 = table.Column<int>(nullable: false),
                    number2 = table.Column<int>(nullable: false),
                    number3 = table.Column<int>(nullable: false),
                    number4 = table.Column<int>(nullable: false),
                    number5 = table.Column<int>(nullable: false),
                    number6 = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_latestDrawLottoNumbers", x => x.latestLottoGameId);
                    table.ForeignKey(
                        name: "FK_latestDrawLottoNumbers_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_latestDrawLottoNumbers_userId",
                table: "latestDrawLottoNumbers",
                column: "userId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "latestDrawLottoNumbers");
        }
    }
}
