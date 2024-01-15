using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenApp.API.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Diaries_DiaryId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Diaries_DiaryId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Calendars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Diaries_DiaryId",
                table: "Calendars",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Diaries_DiaryId",
                table: "Plants",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Diaries_DiaryId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Diaries_DiaryId",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Diaries_DiaryId",
                table: "Calendars",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Diaries_DiaryId",
                table: "Plants",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
