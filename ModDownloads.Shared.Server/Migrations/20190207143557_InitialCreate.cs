using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModDownloads.Shared.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mod",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mod", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Download",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Downloads = table.Column<int>(nullable: false),
                    ModID = table.Column<int>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Download", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Download_Mod_ModID",
                        column: x => x.ModID,
                        principalTable: "Mod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Download_ModID",
                table: "Download",
                column: "ModID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Download");

            migrationBuilder.DropTable(
                name: "Mod");
        }
    }
}
