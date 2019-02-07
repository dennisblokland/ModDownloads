using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModDownloads.Shared.Server.Migrations
{
    public partial class timestamp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Download",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldRowVersion: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "Download",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
