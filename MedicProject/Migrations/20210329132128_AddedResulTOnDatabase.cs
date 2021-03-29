using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MedicProject.Migrations
{
    public partial class AddedResulTOnDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "APPOINTMENTS");

            migrationBuilder.CreateTable(
                name: "result",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    result = table.Column<string>(nullable: true),
                    AppointmentsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_result_APPOINTMENTS_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "APPOINTMENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medicine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    dosage = table.Column<int>(nullable: false),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicine_result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medicine_ResultId",
                table: "medicine",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_result_AppointmentsId",
                table: "result",
                column: "AppointmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medicine");

            migrationBuilder.DropTable(
                name: "result");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "APPOINTMENTS",
                type: "text",
                nullable: true);
        }
    }
}
