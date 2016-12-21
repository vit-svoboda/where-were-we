using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhereWereWe.Repositories.Migrations.SeriesProgress
{
    public partial class UserSeriesBinding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeriesProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Episode = table.Column<int>(nullable: false),
                    Season = table.Column<int>(nullable: false),
                    SeriesId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesProgress_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesProgress_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeriesProgress_SeriesId",
                table: "SeriesProgress",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesProgress_UserId",
                table: "SeriesProgress",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeriesProgress");
        }
    }
}
