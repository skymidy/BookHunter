using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookHunter_Backend.Migrations
{
    public partial class SiteParser_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockInSearchSelector",
                table: "SiteParsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockInSearchSelector",
                table: "SiteParsers");
        }
    }
}
