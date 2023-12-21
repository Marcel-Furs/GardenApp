using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Dodanie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Calendars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Calendars");
        }
    }
}
