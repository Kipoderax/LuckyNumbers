using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyNumbers.API.Migrations
{
    public partial class InitialCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(nullable: true),
                    passwordHash = table.Column<byte[]>(nullable: true),
                    passwordSalt = table.Column<byte[]>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    saldo = table.Column<int>(nullable: false),
                    lastLogin = table.Column<DateTime>(nullable: false),
                    created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "lottoGames",
                columns: table => new
                {
                    lottoGameId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    amountOfThree = table.Column<int>(nullable: false),
                    amountOfFour = table.Column<int>(nullable: false),
                    amountOfFive = table.Column<int>(nullable: false),
                    amountOfSix = table.Column<int>(nullable: false),
                    betsSended = table.Column<int>(nullable: false),
                    maxBetsToSend = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lottoGames", x => x.lottoGameId);
                    table.ForeignKey(
                        name: "FK_lottoGames_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lottoHistoryGames",
                columns: table => new
                {
                    historyLottoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dateGame = table.Column<string>(nullable: true),
                    betsSended = table.Column<int>(nullable: false),
                    amountGoalThrees = table.Column<int>(nullable: false),
                    amountGoalFours = table.Column<int>(nullable: false),
                    amountGoalFives = table.Column<int>(nullable: false),
                    amountGoalSixes = table.Column<int>(nullable: false),
                    experience = table.Column<int>(nullable: false),
                    result = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lottoHistoryGames", x => x.historyLottoId);
                    table.ForeignKey(
                        name: "FK_lottoHistoryGames_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userExperiences",
                columns: table => new
                {
                    userExperienceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    level = table.Column<int>(nullable: false),
                    experience = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userExperiences", x => x.userExperienceId);
                    table.ForeignKey(
                        name: "FK_userExperiences_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userLottoBets",
                columns: table => new
                {
                    lottoBetsId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_userLottoBets", x => x.lottoBetsId);
                    table.ForeignKey(
                        name: "FK_userLottoBets_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lottoGames_userId",
                table: "lottoGames",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lottoHistoryGames_userId",
                table: "lottoHistoryGames",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userExperiences_userId",
                table: "userExperiences",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userLottoBets_userId",
                table: "userLottoBets",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lottoGames");

            migrationBuilder.DropTable(
                name: "lottoHistoryGames");

            migrationBuilder.DropTable(
                name: "userExperiences");

            migrationBuilder.DropTable(
                name: "userLottoBets");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
