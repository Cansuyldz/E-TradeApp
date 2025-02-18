using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_E_ticaretWeb.Migrations
{
    /// <inheritdoc />
    public partial class mig_cart_cartproducts_totalprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "CartProduct",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "CartProduct",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "CartProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartProduct");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartProduct");
        }
    }
}
