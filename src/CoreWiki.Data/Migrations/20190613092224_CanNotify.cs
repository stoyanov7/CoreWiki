using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWiki.Data.Migrations
{
    public partial class CanNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanNotify",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanNotify",
                table: "AspNetUsers");
        }
    }
}
