using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _20121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Users_UserId1",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_UserId1",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Shifts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 7, 40, 30, 492, DateTimeKind.Utc).AddTicks(2585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 7, 38, 20, 88, DateTimeKind.Utc).AddTicks(7799));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(2349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4552));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(1956),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 478, DateTimeKind.Local).AddTicks(1126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 75, DateTimeKind.Local).AddTicks(3100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 487, DateTimeKind.Local).AddTicks(532),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 84, DateTimeKind.Local).AddTicks(2493));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 7, 38, 20, 88, DateTimeKind.Utc).AddTicks(7799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 7, 40, 30, 492, DateTimeKind.Utc).AddTicks(2585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 85, DateTimeKind.Local).AddTicks(4191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 488, DateTimeKind.Local).AddTicks(1956));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "TimeOffRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 75, DateTimeKind.Local).AddTicks(3100),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 478, DateTimeKind.Local).AddTicks(1126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 21, 14, 38, 20, 84, DateTimeKind.Local).AddTicks(2493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 21, 14, 40, 30, 487, DateTimeKind.Local).AddTicks(532));

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_UserId1",
                table: "TimeOffRequests",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId1",
                table: "Shifts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Users_UserId1",
                table: "Shifts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }
    }
}
