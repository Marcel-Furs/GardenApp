using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Aktualizacjav2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_Devices_DeviceId",
                table: "Sensor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensor",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "SensorType",
                table: "Sensor");

            migrationBuilder.RenameTable(
                name: "Sensor",
                newName: "Sensors");

            migrationBuilder.RenameIndex(
                name: "IX_Sensor_DeviceId",
                table: "Sensors",
                newName: "IX_Sensors_DeviceId");

            migrationBuilder.AddColumn<int>(
                name: "SensorTypeId",
                table: "Sensors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SensorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Devices_DeviceId",
                table: "Sensors",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId",
                principalTable: "SensorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Devices_DeviceId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropTable(
                name: "SensorTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensors",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SensorTypeId",
                table: "Sensors");

            migrationBuilder.RenameTable(
                name: "Sensors",
                newName: "Sensor");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_DeviceId",
                table: "Sensor",
                newName: "IX_Sensor_DeviceId");

            migrationBuilder.AddColumn<string>(
                name: "SensorType",
                table: "Sensor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensor",
                table: "Sensor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_Devices_DeviceId",
                table: "Sensor",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
