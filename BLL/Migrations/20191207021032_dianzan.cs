using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Migrations
{
    public partial class dianzan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GetGood",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetGood",
                table: "Blogs");
        }
    }
}
