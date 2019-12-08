using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Migrations
{
    public partial class defaultnonull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GetGood",
                table: "Blogs",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GetGood",
                table: "Blogs",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
