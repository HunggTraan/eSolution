using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _2012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 7, 38, 20, 88, DateTimeKind.Utc).AddTicks(7799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 7, 34, 3, 77, DateTimeKind.Utc).AddTicks(7390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 73, DateTimeKind.Local).AddTicks(7776));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 73, DateTimeKind.Local).AddTicks(7114));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 75, DateTimeKind.Local).AddTicks(3100),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 61, DateTimeKind.Local).AddTicks(1148));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 84, DateTimeKind.Local).AddTicks(2493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 71, DateTimeKind.Local).AddTicks(9620));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 7, 34, 3, 77, DateTimeKind.Utc).AddTicks(7390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 7, 38, 20, 88, DateTimeKind.Utc).AddTicks(7799));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 73, DateTimeKind.Local).AddTicks(7776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4552));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 73, DateTimeKind.Local).AddTicks(7114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 61, DateTimeKind.Local).AddTicks(1148),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 75, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 34, 3, 71, DateTimeKind.Local).AddTicks(9620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 84, DateTimeKind.Local).AddTicks(2493));
        }
    }
}
