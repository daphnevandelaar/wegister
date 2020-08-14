using Microsoft.EntityFrameworkCore.Migrations;

namespace Wegister.DAL.Migrations
{
    public partial class Add_New_Employer_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "HourRegistrations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourRegistrations_EmployerId",
                table: "HourRegistrations",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HourRegistrations_Employers_EmployerId",
                table: "HourRegistrations",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HourRegistrations_Employers_EmployerId",
                table: "HourRegistrations");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_HourRegistrations_EmployerId",
                table: "HourRegistrations");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "HourRegistrations");
        }
    }
}
