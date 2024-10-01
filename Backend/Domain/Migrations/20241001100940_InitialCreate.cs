using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false),
                    RolBeschrijving = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('Nieuwe Positie')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Werven",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Werven", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Werknemer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Achternaam = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Emailadres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Wachtwoord = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    WerfId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    RolId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Werknemer", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Werknemer_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Werknemer_Werven_WerfId",
                        column: x => x.WerfId,
                        principalTable: "Werven",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WerknemerId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    VervalDatum = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Werknemer_WerknemerId",
                        column: x => x.WerknemerId,
                        principalTable: "Werknemer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_WerknemerId",
                table: "RefreshToken",
                column: "WerknemerId");

            migrationBuilder.CreateIndex(
                name: "IX_Werknemer_RolId",
                table: "Werknemer",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Werknemer_WerfId",
                table: "Werknemer",
                column: "WerfId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Werknemer");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Werven");
        }
    }
}
