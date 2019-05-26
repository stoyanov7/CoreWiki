using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Data.Migrations
{
    public partial class UniqueSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Slug",
                table: "Articles",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_Slug",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
