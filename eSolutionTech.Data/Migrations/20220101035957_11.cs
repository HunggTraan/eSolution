using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExceedTimeOut",
                table: "ShiftSettings",
                newName: "RestTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 3, 59, 57, 339, DateTimeKind.Utc).AddTicks(4501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 4, 47, 58, 361, DateTimeKind.Utc).AddTicks(9609));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1734),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 358, DateTimeKind.Local).AddTicks(6432));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 358, DateTimeKind.Local).AddTicks(6054));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 325, DateTimeKind.Local).AddTicks(3231),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 347, DateTimeKind.Local).AddTicks(8441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 334, DateTimeKind.Local).AddTicks(9601),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 357, DateTimeKind.Local).AddTicks(3918));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RestTime",
                table: "ShiftSettings",
                newName: "ExceedTimeOut");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 4, 47, 58, 361, DateTimeKind.Utc).AddTicks(9609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 3, 59, 57, 339, DateTimeKind.Utc).AddTicks(4501));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 358, DateTimeKind.Local).AddTicks(6432),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 358, DateTimeKind.Local).AddTicks(6054),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 347, DateTimeKind.Local).AddTicks(8441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 325, DateTimeKind.Local).AddTicks(3231));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 11, 47, 58, 357, DateTimeKind.Local).AddTicks(3918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 334, DateTimeKind.Local).AddTicks(9601));
        }
    }
}
