using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wegister.DAL.Migrations
{
    public partial class WorkweekStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "WorkWeeks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkWeeks_StatusId",
                table: "WorkWeeks",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkWeeks_Status_StatusId",
                table: "WorkWeeks",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkWeeks_Status_StatusId",
                table: "WorkWeeks");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_WorkWeeks_StatusId",
                table: "WorkWeeks");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "WorkWeeks");
        }
    }
}
