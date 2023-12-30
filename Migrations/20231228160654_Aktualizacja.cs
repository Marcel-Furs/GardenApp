using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Aktualizacja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Plants_PlantId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_PlantProfiles_PlantProfileId",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Devices_PlantId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "PlantProfileId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SensorValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_DeviceId",
                table: "Plants",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_DeviceId",
                table: "Sensor",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Devices_DeviceId",
                table: "Plants",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_PlantProfiles_PlantProfileId",
                table: "Plants",
                column: "PlantProfileId",
                principalTable: "PlantProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Devices_DeviceId",
                table: "Plants");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_PlantProfiles_PlantProfileId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropIndex(
                name: "IX_Plants_DeviceId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "PlantProfileId",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PlantId",
                table: "Devices",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Plants_PlantId",
                table: "Devices",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_PlantProfiles_PlantProfileId",
                table: "Plants",
                column: "PlantProfileId",
                principalTable: "PlantProfiles",
                principalColumn: "Id");
        }
    }
}
