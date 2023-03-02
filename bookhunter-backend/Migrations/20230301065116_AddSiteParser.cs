using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookHunter_Backend.Migrations
{
    public partial class AddSiteParser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteParsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsbnSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageCountSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagsSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorsSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenresSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkInSearchSelector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchLinkFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInSearchSelector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteParsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteParsers");
        }
    }
}
