using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicProject.Migrations
{
    public partial class NullMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS");

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS");

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
