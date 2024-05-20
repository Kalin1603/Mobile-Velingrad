using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Velingrad.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionFromBrandAndModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Brands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Models",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
