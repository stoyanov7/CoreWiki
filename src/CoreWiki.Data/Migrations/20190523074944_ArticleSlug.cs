using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Data.Migrations
{
    public partial class ArticleSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Articles",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
