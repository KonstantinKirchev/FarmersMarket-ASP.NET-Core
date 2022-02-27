using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarket.Data.Migrations
{
    public partial class AddedManagersToFarms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FarmId",
                table: "AspNetUsers",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Farms_FarmId",
                table: "AspNetUsers",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Farms_FarmId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FarmId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "AspNetUsers");
        }
    }
}
