using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wegister.DAL.Migrations
{
    public partial class ChangedEntitiesIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Status",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "HourRegistrations",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Employers",
                nullable: false,
                defaultValue: Guid.NewGuid());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "HourRegistrations");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Employers");
        }
    }
}
