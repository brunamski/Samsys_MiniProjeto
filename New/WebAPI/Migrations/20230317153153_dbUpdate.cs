using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class dbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_authorId",
                table: "Books",
                column: "authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "authorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_authorId",
                table: "Books");
        }
    }
}
