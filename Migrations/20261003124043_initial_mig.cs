using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeriA_Football.API.Migrations
{
    /// <inheritdoc />
    public partial class initial_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeScore = table.Column<int>(type: "int", nullable: true),
                    AwayScore = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.CheckConstraint("CK_Match_DifferentTeams", "[HomeTeamId] <> [AwayTeamId]");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchCards_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchCards_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchGoals_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchGoals_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Substitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    PlayerIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerOut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Substitutions_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitutions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "LogoUrl", "Name", "Stadium" },
                values: new object[,]
                {
                    { 1, "Bergamo", "/images/logos/atalanta.png", "Atalanta", "Gewiss Stadium" },
                    { 2, "Bologna", "/images/logos/bologna.png", "Bologna", "Stadio Renato Dall'Ara" },
                    { 3, "Cagliari", "/images/logos/cagliari.png", "Cagliari", "Unipol Domus" },
                    { 4, "Como", "/images/logos/como.png", "Como", "Stadio Giuseppe Sinigaglia" },
                    { 5, "Cremona", "/images/logos/cremonese.png", "Cremonese", "Stadio Giovanni Zini" },
                    { 6, "Floransa", "/images/logos/fiorentina.png", "Fiorentina", "Stadio Artemio Franchi" },
                    { 7, "Cenova", "/images/logos/genoa.png", "Genoa", "Stadio Luigi Ferraris" },
                    { 8, "Verona", "/images/logos/verona.png", "Hellas Verona", "Stadio Marcantonio Bentegodi" },
                    { 9, "Milano", "/images/logos/inter.png", "Inter", "San Siro" },
                    { 10, "Torino", "/images/logos/juventus.png", "Juventus", "Allianz Stadium" },
                    { 11, "Roma", "/images/logos/lazio.png", "Lazio", "Stadio Olimpico" },
                    { 12, "Lecce", "/images/logos/lecce.png", "Lecce", "Stadio Via del Mare" },
                    { 13, "Milano", "/images/logos/milan.png", "Milan", "San Siro" },
                    { 14, "Napoli", "/images/logos/napoli.png", "Napoli", "Stadio Diego Armando Maradona" },
                    { 15, "Parma", "/images/logos/parma.png", "Parma", "Stadio Ennio Tardini" },
                    { 16, "Pisa", "/images/logos/pisa.png", "Pisa", "Arena Garibaldi" },
                    { 17, "Roma", "/images/logos/roma.png", "Roma", "Stadio Olimpico" },
                    { 18, "Sassuolo", "/images/logos/sassuolo.png", "Sassuolo", "Mapei Stadium" },
                    { 19, "Torino", "/images/logos/torino.png", "Torino", "Stadio Olimpico Grande Torino" },
                    { 20, "Udine", "/images/logos/udinese.png", "Udinese", "Bluenergy Stadium" }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayScore", "AwayTeamId", "HomeScore", "HomeTeamId", "MatchDate", "Stadium", "Status", "Week" },
                values: new object[,]
                {
                    { 1, 1, 19, 3, 9, new DateTime(2026, 8, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), "San Siro", 2, 1 },
                    { 2, 0, 15, 2, 10, new DateTime(2026, 8, 23, 20, 45, 0, 0, DateTimeKind.Unspecified), "Allianz Stadium", 2, 1 },
                    { 3, 1, 18, 2, 14, new DateTime(2026, 8, 22, 18, 30, 0, 0, DateTimeKind.Unspecified), "Stadio Diego Armando Maradona", 2, 1 },
                    { 4, 1, 2, 1, 17, new DateTime(2026, 8, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Olimpico", 2, 1 },
                    { 5, 0, 16, 4, 1, new DateTime(2026, 8, 23, 15, 0, 0, 0, DateTimeKind.Unspecified), "Gewiss Stadium", 2, 1 },
                    { 6, 0, 3, 1, 6, new DateTime(2026, 8, 23, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Artemio Franchi", 2, 1 },
                    { 7, 2, 13, 1, 5, new DateTime(2026, 8, 22, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Giovanni Zini", 2, 1 },
                    { 8, 0, 11, 0, 8, new DateTime(2026, 8, 24, 20, 45, 0, 0, DateTimeKind.Unspecified), "Stadio Marcantonio Bentegodi", 2, 1 },
                    { 9, 2, 7, 2, 12, new DateTime(2026, 8, 24, 18, 30, 0, 0, DateTimeKind.Unspecified), "Stadio Via del Mare", 2, 1 },
                    { 10, 1, 4, 0, 20, new DateTime(2026, 8, 23, 12, 30, 0, 0, DateTimeKind.Unspecified), "Bluenergy Stadium", 2, 1 },
                    { 11, 0, 20, 2, 13, new DateTime(2026, 8, 29, 20, 45, 0, 0, DateTimeKind.Unspecified), "San Siro", 2, 2 },
                    { 12, 3, 14, 1, 11, new DateTime(2026, 8, 30, 20, 45, 0, 0, DateTimeKind.Unspecified), "Stadio Olimpico", 2, 2 },
                    { 13, 1, 1, 1, 19, new DateTime(2026, 8, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Olimpico Grande Torino", 2, 2 },
                    { 14, 2, 17, 0, 16, new DateTime(2026, 8, 29, 18, 30, 0, 0, DateTimeKind.Unspecified), "Arena Garibaldi", 2, 2 },
                    { 15, 1, 10, 1, 7, new DateTime(2026, 8, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Luigi Ferraris", 2, 2 },
                    { 16, 2, 9, 0, 4, new DateTime(2026, 8, 29, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Giuseppe Sinigaglia", 2, 2 },
                    { 17, 1, 6, 2, 2, new DateTime(2026, 8, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), "Stadio Renato Dall'Ara", 2, 2 },
                    { 18, 0, 8, 2, 3, new DateTime(2026, 8, 30, 12, 30, 0, 0, DateTimeKind.Unspecified), "Unipol Domus", 2, 2 },
                    { 19, 2, 5, 3, 18, new DateTime(2026, 8, 31, 18, 30, 0, 0, DateTimeKind.Unspecified), "Mapei Stadium", 2, 2 },
                    { 20, 0, 12, 1, 15, new DateTime(2026, 8, 30, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Ennio Tardini", 2, 2 },
                    { 21, 1, 11, 2, 9, new DateTime(2026, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), "San Siro", 2, 3 },
                    { 22, 1, 13, 1, 17, new DateTime(2026, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), "Stadio Olimpico", 2, 3 },
                    { 23, 1, 1, 0, 10, new DateTime(2026, 9, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), "Allianz Stadium", 2, 3 },
                    { 24, 0, 7, 3, 14, new DateTime(2026, 9, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Diego Armando Maradona", 2, 3 },
                    { 25, 2, 19, 2, 6, new DateTime(2026, 9, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Artemio Franchi", 2, 3 },
                    { 26, 0, 3, 1, 2, new DateTime(2026, 9, 13, 18, 0, 0, 0, DateTimeKind.Unspecified), "Stadio Renato Dall'Ara", 2, 3 },
                    { 27, null, 15, null, 20, new DateTime(2026, 9, 14, 16, 30, 0, 0, DateTimeKind.Unspecified), "Bluenergy Stadium", 0, 3 },
                    { 28, null, 18, null, 8, new DateTime(2026, 9, 14, 20, 45, 0, 0, DateTimeKind.Unspecified), "Stadio Marcantonio Bentegodi", 0, 3 },
                    { 29, null, 16, null, 12, new DateTime(2026, 9, 14, 18, 30, 0, 0, DateTimeKind.Unspecified), "Stadio Via del Mare", 0, 3 },
                    { 30, null, 4, null, 5, new DateTime(2026, 9, 14, 20, 45, 0, 0, DateTimeKind.Unspecified), "Stadio Giovanni Zini", 0, 3 }
                });

            migrationBuilder.InsertData(
                table: "MatchCards",
                columns: new[] { "Id", "CardType", "MatchId", "Minute", "PlayerName", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, 1, 27, "Saúl Coco", 19 },
                    { 2, 1, 1, 44, "Hakan Çalhanoğlu", 9 },
                    { 3, 1, 1, 69, "Kristjan Asllani", 19 },
                    { 4, 1, 12, 30, "Matteo Guendouzi", 11 },
                    { 5, 1, 12, 52, "Stanislav Lobotka", 14 },
                    { 6, 1, 12, 74, "Alessio Romagnoli", 11 },
                    { 7, 2, 12, 79, "Alessio Romagnoli", 11 },
                    { 8, 1, 19, 19, "Nemanja Matić", 18 },
                    { 9, 1, 19, 56, "Warren Bondo", 5 },
                    { 10, 1, 19, 90, "Franco Vázquez", 5 },
                    { 11, 1, 22, 15, "Gianluca Mancini", 17 },
                    { 12, 1, 22, 50, "Youssouf Fofana", 13 },
                    { 13, 1, 22, 61, "Bryan Cristante", 17 },
                    { 14, 3, 22, 89, "Fikayo Tomori", 13 }
                });

            migrationBuilder.InsertData(
                table: "MatchGoals",
                columns: new[] { "Id", "MatchId", "Minute", "PlayerName", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, 12, "Lautaro Martínez", 9 },
                    { 2, 1, 34, "Giovanni Simeone", 19 },
                    { 3, 1, 58, "Marcus Thuram", 9 },
                    { 4, 1, 81, "Nicolò Barella", 9 },
                    { 5, 12, 21, "Scott McTominay", 14 },
                    { 6, 12, 39, "Mattia Zaccagni", 11 },
                    { 7, 12, 55, "Rasmus Højlund", 14 },
                    { 8, 12, 87, "Matteo Politano", 14 },
                    { 9, 19, 8, "Domenico Berardi", 18 },
                    { 10, 19, 25, "Jamie Vardy", 5 },
                    { 11, 19, 47, "Andrea Pinamonti", 18 },
                    { 12, 19, 63, "Federico Bonazzoli", 5 },
                    { 13, 19, 85, "Armand Laurienté", 18 },
                    { 14, 22, 38, "Paulo Dybala", 17 },
                    { 15, 22, 72, "Rafael Leão", 13 }
                });

            migrationBuilder.InsertData(
                table: "Substitutions",
                columns: new[] { "Id", "MatchId", "Minute", "PlayerIn", "PlayerOut", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, 60, "Duván Zapata", "Giovanni Simeone", 19 },
                    { 2, 1, 66, "Cyril Ngonge", "Nikola Vlašić", 19 },
                    { 3, 1, 70, "Ange-Yoan Bonny", "Marcus Thuram", 9 },
                    { 4, 1, 75, "Davide Frattesi", "Hakan Çalhanoğlu", 9 },
                    { 5, 1, 84, "Piotr Zieliński", "Nicolò Barella", 9 },
                    { 6, 12, 61, "Boulaye Dia", "Valentín Castellanos", 11 },
                    { 7, 12, 66, "David Neres", "Kevin De Bruyne", 14 },
                    { 8, 12, 70, "Pedro", "Gustav Isaksen", 11 },
                    { 9, 12, 80, "Billy Gilmour", "Stanislav Lobotka", 14 },
                    { 10, 19, 68, "Cristian Volpato", "Domenico Berardi", 18 },
                    { 11, 19, 70, "Antonio Sanabria", "Jamie Vardy", 5 },
                    { 12, 19, 76, "Alberto Grassi", "Warren Bondo", 5 },
                    { 13, 19, 82, "Luca Lipani", "Nemanja Matić", 18 },
                    { 14, 22, 60, "Christopher Nkunku", "Santiago Giménez", 13 },
                    { 15, 22, 65, "Evan Ferguson", "Artem Dovbyk", 17 },
                    { 16, 22, 70, "Ruben Loftus-Cheek", "Luka Modrić", 13 },
                    { 17, 22, 75, "Stephan El Shaarawy", "Paulo Dybala", 17 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchCards_MatchId",
                table: "MatchCards",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchCards_TeamId",
                table: "MatchCards",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchGoals_MatchId",
                table: "MatchGoals",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchGoals_TeamId",
                table: "MatchGoals",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutions_MatchId",
                table: "Substitutions",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutions_TeamId",
                table: "Substitutions",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchCards");

            migrationBuilder.DropTable(
                name: "MatchGoals");

            migrationBuilder.DropTable(
                name: "Substitutions");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
