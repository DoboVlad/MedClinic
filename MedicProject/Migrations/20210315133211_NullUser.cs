using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicProject.Migrations
{
    public partial class NullUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "APPOINTMENTS",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "APPOINTMENTS",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
