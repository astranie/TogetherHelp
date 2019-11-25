using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Migrations
{
    public partial class post3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
