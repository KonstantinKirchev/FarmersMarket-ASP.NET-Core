using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarket.Data.Migrations
{
    public partial class AddedNewPropertyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HowdoYouKnowAboutUs",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HowdoYouKnowAboutUs",
                table: "AspNetUsers");
        }
    }
}
