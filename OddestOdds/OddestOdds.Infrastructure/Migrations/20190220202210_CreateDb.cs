using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OddestOdds.Infrastructure.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OddValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HomeOddValue = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    DrawOddValue = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AwayOddValue = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OddValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OddName = table.Column<string>(nullable: false),
                    HomeTeamName = table.Column<string>(nullable: false),
                    AwayTeamName = table.Column<string>(nullable: false),
                    OddValueId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odds_OddValues_OddValueId",
                        column: x => x.OddValueId,
                        principalTable: "OddValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odds_OddValueId",
                table: "Odds",
                column: "OddValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odds");

            migrationBuilder.DropTable(
                name: "OddValues");
        }
    }
}
