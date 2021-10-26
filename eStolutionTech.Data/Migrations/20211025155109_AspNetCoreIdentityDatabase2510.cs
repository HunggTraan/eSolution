using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class AspNetCoreIdentityDatabase2510 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Shifts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 275, DateTimeKind.Utc).AddTicks(7665),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 146, DateTimeKind.Utc).AddTicks(6719));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 140, DateTimeKind.Local).AddTicks(7836));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8311),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 140, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TimeOffRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOut",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2072),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(3318));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartIn",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(1620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(2921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOut",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(3793));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndIn",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2312),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(3567));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 255, DateTimeKind.Local).AddTicks(3452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 130, DateTimeKind.Local).AddTicks(5407));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 264, DateTimeKind.Local).AddTicks(5503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 139, DateTimeKind.Local).AddTicks(5263));

            migrationBuilder.CreateIndex(
                name: "IX_TimeOffRequests_UserId",
                table: "TimeOffRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Users_UserId",
                table: "Shifts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffRequests_Users_UserId",
                table: "TimeOffRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Users_UserId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffRequests_Users_UserId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeOffRequests_UserId",
                table: "TimeOffRequests");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeOffRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shifts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 146, DateTimeKind.Utc).AddTicks(6719),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 275, DateTimeKind.Utc).AddTicks(7665));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 140, DateTimeKind.Local).AddTicks(7836),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 140, DateTimeKind.Local).AddTicks(7440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8311));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "TimeOffRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "TimeOffRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOut",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(3318),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2072));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartIn",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(2921),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOut",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(3793),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndIn",
                table: "ShiftTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 38, 23, 145, DateTimeKind.Utc).AddTicks(3567),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2312));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 130, DateTimeKind.Local).AddTicks(5407),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 255, DateTimeKind.Local).AddTicks(3452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 38, 23, 139, DateTimeKind.Local).AddTicks(5263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 264, DateTimeKind.Local).AddTicks(5503));
        }
    }
}
