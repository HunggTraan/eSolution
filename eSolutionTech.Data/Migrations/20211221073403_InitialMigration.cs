using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSolutionTech.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeOffRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOffType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HalfDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
