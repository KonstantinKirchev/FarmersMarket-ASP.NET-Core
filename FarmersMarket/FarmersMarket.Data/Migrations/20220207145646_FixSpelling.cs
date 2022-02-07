using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarket.Data.Migrations
{
    public partial class FixSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HowdoYouKnowAboutUs",
                table: "AspNetUsers",
                newName: "HowDoYouKnowAboutUs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HowDoYouKnowAboutUs",
                table: "AspNetUsers",
                newName: "HowdoYouKnowAboutUs");
        }
    }
}
