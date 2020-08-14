using Microsoft.EntityFrameworkCore.Migrations;

namespace Wegister.DAL.Migrations
{
    public partial class HourregistrationAddedTotalToList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalWorkedHoursDayInSeconds",
                table: "HourRegistrations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE HourRegistrations SET TotalWorkedHoursDayInSeconds = DATEDIFF(second, StartTime, EndTime) - (60*Recreation) WHERE Id IS NOT NULL"); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalWorkedHoursDayInSeconds",
                table: "HourRegistrations");
        }
    }
}
