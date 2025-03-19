using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_E_ticaretWeb.Migrations
{
    /// <inheritdoc />
    public partial class mignewoder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Streetaddress",
                table: "OrderProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Streetaddress",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
