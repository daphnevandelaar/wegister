using Microsoft.EntityFrameworkCore.Migrations;

namespace Wegister.DAL.Migrations
{
    public partial class AddedEmailToEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employers");
        }
    }
}
