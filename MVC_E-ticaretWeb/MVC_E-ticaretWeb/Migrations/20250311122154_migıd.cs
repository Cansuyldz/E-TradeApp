using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_E_ticaretWeb.Migrations
{
    /// <inheritdoc />
    public partial class migıd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CartId",
                table: "Adresses",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Cart_CartId",
                table: "Adresses",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Cart_CartId",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_CartId",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Adresses");
        }
    }
}
