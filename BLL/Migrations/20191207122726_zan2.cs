using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Migrations
{
    public partial class zan2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogsAndGooders",
                columns: table => new
                {
                    GooderId = table.Column<int>(nullable: false),
                    BlogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsAndGooders", x => new { x.GooderId, x.BlogId });
                    table.ForeignKey(
                        name: "FK_BlogsAndGooders_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogsAndGooders_Users_GooderId",
                        column: x => x.GooderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogsAndGooders_BlogId",
                table: "BlogsAndGooders",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogsAndGooders");
        }
    }
}
