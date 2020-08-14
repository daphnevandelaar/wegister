using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wegister.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WeekNumber = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HourRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Recreation = table.Column<int>(nullable: false),
                    WorkWeekId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourRegistrations_WorkWeeks_WorkWeekId",
                        column: x => x.WorkWeekId,
                        principalTable: "WorkWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourRegistrations_WorkWeekId",
                table: "HourRegistrations",
                column: "WorkWeekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourRegistrations");

            migrationBuilder.DropTable(
                name: "WorkWeeks");
        }
    }
}
