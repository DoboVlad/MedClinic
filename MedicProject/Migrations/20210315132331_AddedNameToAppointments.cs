using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicProject.Migrations
{
    public partial class AddedNameToAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "APPOINTMENTS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "APPOINTMENTS");
        }
    }
}
