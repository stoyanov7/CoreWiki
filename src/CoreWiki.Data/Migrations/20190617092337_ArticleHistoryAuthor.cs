using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Data.Migrations
{
    public partial class ArticleHistoryAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "ArticleHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHistories_AuthorId",
                table: "ArticleHistories",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleHistories_AspNetUsers_AuthorId",
                table: "ArticleHistories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleHistories_AspNetUsers_AuthorId",
                table: "ArticleHistories");

            migrationBuilder.DropIndex(
                name: "IX_ArticleHistories_AuthorId",
                table: "ArticleHistories");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "ArticleHistories");
        }
    }
}
