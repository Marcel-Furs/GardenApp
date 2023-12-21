using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenApp.API.Migrations
{
    /// <inheritdoc />
    public partial class polaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Calendars",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Calendars",
                newName: "description");
        }
    }
}
