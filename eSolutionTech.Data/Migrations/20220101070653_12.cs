using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JobTitleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 7, 6, 53, 369, DateTimeKind.Utc).AddTicks(4316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 3, 59, 57, 339, DateTimeKind.Utc).AddTicks(4501));

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8599),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 355, DateTimeKind.Local).AddTicks(2769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 325, DateTimeKind.Local).AddTicks(3231));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 364, DateTimeKind.Local).AddTicks(5911),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 334, DateTimeKind.Local).AddTicks(9601));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobTitleId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 3, 59, 57, 339, DateTimeKind.Utc).AddTicks(4501),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 7, 6, 53, 369, DateTimeKind.Utc).AddTicks(4316));

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1734),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 336, DateTimeKind.Local).AddTicks(1358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8599));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 325, DateTimeKind.Local).AddTicks(3231),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 355, DateTimeKind.Local).AddTicks(2769));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 10, 59, 57, 334, DateTimeKind.Local).AddTicks(9601),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 364, DateTimeKind.Local).AddTicks(5911));
        }
    }
}
