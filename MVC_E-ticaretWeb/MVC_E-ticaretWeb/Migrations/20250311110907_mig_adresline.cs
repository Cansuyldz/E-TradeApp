using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_E_ticaretWeb.Migrations
{
    /// <inheritdoc />
    public partial class mig_adresline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Adresses",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Adresses",
                newName: "Streetaddress");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Adresses",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Adresses",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Neighbourhood",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Neighbourhood",
                table: "Adresses");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Adresses",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Streetaddress",
                table: "Adresses",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Adresses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Adresses",
                newName: "City");
        }
    }
}
