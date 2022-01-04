using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 7, 25, 34, 189, DateTimeKind.Utc).AddTicks(6593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 7, 23, 30, 74, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 185, DateTimeKind.Local).AddTicks(6034),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(7185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 185, DateTimeKind.Local).AddTicks(5640),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(6801));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 175, DateTimeKind.Local).AddTicks(4775),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 60, DateTimeKind.Local).AddTicks(3066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 184, DateTimeKind.Local).AddTicks(3929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 69, DateTimeKind.Local).AddTicks(4031));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 7, 23, 30, 74, DateTimeKind.Utc).AddTicks(5418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 7, 25, 34, 189, DateTimeKind.Utc).AddTicks(6593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(7185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 185, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(6801),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 185, DateTimeKind.Local).AddTicks(5640));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 60, DateTimeKind.Local).AddTicks(3066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 175, DateTimeKind.Local).AddTicks(4775));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 69, DateTimeKind.Local).AddTicks(4031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 25, 34, 184, DateTimeKind.Local).AddTicks(3929));
        }
    }
}
