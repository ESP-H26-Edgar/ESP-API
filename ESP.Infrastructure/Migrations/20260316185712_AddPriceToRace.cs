using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToRace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Racetypes",
                columns: table => new
                {
                    IdRaceType = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RaceType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRaceType);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Nationality = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubTeam = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mail = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdUser);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    IdRace = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RaceName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdRaceType = table.Column<int>(type: "int(11)", nullable: false),
                    Kilometer = table.Column<int>(type: "int(11)", nullable: false),
                    Location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberPlace = table.Column<int>(type: "int(11)", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRace);
                    table.ForeignKey(
                        name: "races_ibfk_1",
                        column: x => x.IdRaceType,
                        principalTable: "Racetypes",
                        principalColumn: "IdRaceType");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    IdComment = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdRace = table.Column<int>(type: "int(11)", nullable: false),
                    Rate = table.Column<int>(type: "int(11)", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdComment);
                    table.ForeignKey(
                        name: "comments_ibfk_1",
                        column: x => x.IdRace,
                        principalTable: "Races",
                        principalColumn: "IdRace");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    IdRegistration = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<int>(type: "int(11)", nullable: false),
                    IdRace = table.Column<int>(type: "int(11)", nullable: false),
                    BibNumber = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRegistration);
                    table.ForeignKey(
                        name: "registrations_ibfk_1",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "registrations_ibfk_2",
                        column: x => x.IdRace,
                        principalTable: "Races",
                        principalColumn: "IdRace");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    IdResult = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<int>(type: "int(11)", nullable: false),
                    IdRace = table.Column<int>(type: "int(11)", nullable: false),
                    Place = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdResult);
                    table.ForeignKey(
                        name: "results_ibfk_1",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "results_ibfk_2",
                        column: x => x.IdRace,
                        principalTable: "Races",
                        principalColumn: "IdRace");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IdRace",
                table: "Comments",
                column: "IdRace");

            migrationBuilder.CreateIndex(
                name: "IdRaceType",
                table: "Races",
                column: "IdRaceType");

            migrationBuilder.CreateIndex(
                name: "IdRace1",
                table: "Registrations",
                column: "IdRace");

            migrationBuilder.CreateIndex(
                name: "IdUser",
                table: "Registrations",
                columns: new[] { "IdUser", "IdRace" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IdRace2",
                table: "Results",
                column: "IdRace");

            migrationBuilder.CreateIndex(
                name: "IdUser1",
                table: "Results",
                columns: new[] { "IdUser", "IdRace" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Mail",
                table: "Users",
                column: "Mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Racetypes");
        }
    }
}
