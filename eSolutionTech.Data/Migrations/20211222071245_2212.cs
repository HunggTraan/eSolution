using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _2212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 22, 7, 12, 44, 985, DateTimeKind.Utc).AddTicks(8427),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 7, 40, 30, 492, DateTimeKind.Utc).AddTicks(2585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 982, DateTimeKind.Local).AddTicks(215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 981, DateTimeKind.Local).AddTicks(9795),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(1956));

            migrationBuilder.AddColumn<int>(
                name: "isLate",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isLogin",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 969, DateTimeKind.Local).AddTicks(6528),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 478, DateTimeKind.Local).AddTicks(1126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 980, DateTimeKind.Local).AddTicks(6114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 487, DateTimeKind.Local).AddTicks(532));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLate",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "isLogin",
                table: "Shifts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 7, 40, 30, 492, DateTimeKind.Utc).AddTicks(2585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 22, 7, 12, 44, 985, DateTimeKind.Utc).AddTicks(8427));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(2349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 982, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(1956),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 981, DateTimeKind.Local).AddTicks(9795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 478, DateTimeKind.Local).AddTicks(1126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 969, DateTimeKind.Local).AddTicks(6528));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 487, DateTimeKind.Local).AddTicks(532),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 22, 14, 12, 44, 980, DateTimeKind.Local).AddTicks(6114));
        }
    }
}
