using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Migrations
{
    public partial class imagepath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderImage",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "HeaderPath",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderPath",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "HeaderImage",
                table: "Users",
                nullable: true);
        }
    }
}
