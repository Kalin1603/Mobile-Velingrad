using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Velingrad.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDescriptionFromTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
