using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 7, 23, 30, 74, DateTimeKind.Utc).AddTicks(5418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 7, 6, 53, 369, DateTimeKind.Utc).AddTicks(4316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(7185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(6801),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8599));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TimeOffRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TimeOffType",
                table: "TimeOffRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 60, DateTimeKind.Local).AddTicks(3066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 355, DateTimeKind.Local).AddTicks(2769));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 69, DateTimeKind.Local).AddTicks(4031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 364, DateTimeKind.Local).AddTicks(5911));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 7, 6, 53, 369, DateTimeKind.Utc).AddTicks(4316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 7, 23, 30, 74, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(7185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 365, DateTimeKind.Local).AddTicks(8599),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 70, DateTimeKind.Local).AddTicks(6801));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "TimeOffType",
                table: "TimeOffRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 355, DateTimeKind.Local).AddTicks(2769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 60, DateTimeKind.Local).AddTicks(3066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 14, 6, 53, 364, DateTimeKind.Local).AddTicks(5911),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 1, 14, 23, 30, 69, DateTimeKind.Local).AddTicks(4031));
        }
    }
}
