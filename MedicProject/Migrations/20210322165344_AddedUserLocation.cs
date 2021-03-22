using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicProject.Migrations
{
    public partial class AddedUserLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeNumber",
                table: "users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "APPOINTMENTS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "users");

            migrationBuilder.DropColumn(
                name: "County",
                table: "users");

            migrationBuilder.DropColumn(
                name: "HomeNumber",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "APPOINTMENTS");
        }
    }
}
