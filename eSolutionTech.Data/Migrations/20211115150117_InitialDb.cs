using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftTypes");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Departments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 15, 1, 16, 735, DateTimeKind.Utc).AddTicks(8773),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 275, DateTimeKind.Utc).AddTicks(7665));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 728, DateTimeKind.Local).AddTicks(6628),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 728, DateTimeKind.Local).AddTicks(6260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 718, DateTimeKind.Local).AddTicks(1548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 255, DateTimeKind.Local).AddTicks(3452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 727, DateTimeKind.Local).AddTicks(2958),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 264, DateTimeKind.Local).AddTicks(5503));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndIn",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 15, 1, 16, 727, DateTimeKind.Utc).AddTicks(4760));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndOut",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 15, 1, 16, 727, DateTimeKind.Utc).AddTicks(4981));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartIn",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 15, 1, 16, 727, DateTimeKind.Utc).AddTicks(4127));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartOut",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 15, 15, 1, 16, 727, DateTimeKind.Utc).AddTicks(4510));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndIn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EndOut",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartIn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartOut",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 275, DateTimeKind.Utc).AddTicks(7665),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 15, 15, 1, 16, 735, DateTimeKind.Utc).AddTicks(8773));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 728, DateTimeKind.Local).AddTicks(6628));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "TimeOffTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 265, DateTimeKind.Local).AddTicks(8311),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 728, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 255, DateTimeKind.Local).AddTicks(3452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 718, DateTimeKind.Local).AddTicks(1548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 25, 22, 51, 9, 264, DateTimeKind.Local).AddTicks(5503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 15, 22, 1, 16, 727, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.AddColumn<string>(
                name: "ShiftId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShiftTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndIn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2312)),
                    EndOut = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2540)),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartIn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(1620)),
                    StartOut = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 25, 15, 51, 9, 274, DateTimeKind.Utc).AddTicks(2072))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftTypes", x => x.Id);
                });
        }
    }
}
