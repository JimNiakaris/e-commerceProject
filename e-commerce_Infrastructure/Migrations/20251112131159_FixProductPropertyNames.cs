using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProductPropertyNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PrictureUrl",
                table: "Products",
                newName: "PictureUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Products",
                newName: "PrictureUrl");
        }
    }
}
