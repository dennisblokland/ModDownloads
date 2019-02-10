using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModDownloads.Server.Migrations
{
    public partial class removeincrease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadIncrease");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DownloadIncrease",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Increase = table.Column<int>(nullable: false),
                    ModId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadIncrease", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DownloadIncrease_Mod_ModId",
                        column: x => x.ModId,
                        principalTable: "Mod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadIncrease_ModId",
                table: "DownloadIncrease",
                column: "ModId");
        }
    }
}
