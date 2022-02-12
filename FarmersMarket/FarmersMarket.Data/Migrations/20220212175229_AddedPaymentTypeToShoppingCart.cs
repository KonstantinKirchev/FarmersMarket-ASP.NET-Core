using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmersMarket.Data.Migrations
{
    public partial class AddedPaymentTypeToShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "ShoppingCarts");
        }
    }
}
