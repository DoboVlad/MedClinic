using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicProject.Migrations
{
    public partial class AddedMoreLocationInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Appartment",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Entrance",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appartment",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Entrance",
                table: "users");
        }
    }
}
