using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MedicProject.Migrations
{
    public partial class AddedMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_USERS_UserId",
                table: "APPOINTMENTS");

            migrationBuilder.DropForeignKey(
                name: "FK_USERS_USERS_doctorId",
                table: "USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS",
                table: "USERS");

            migrationBuilder.RenameTable(
                name: "USERS",
                newName: "users");

            migrationBuilder.RenameIndex(
                name: "IX_USERS_doctorId",
                table: "users",
                newName: "IX_users_doctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TransmitterId = table.Column<int>(nullable: false),
                    TransmitterEmail = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<int>(nullable: false),
                    ReceiverEmail = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DateRead = table.Column<DateTime>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_messages_users_TransmitterId",
                        column: x => x.TransmitterId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_ReceiverId",
                table: "messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_TransmitterId",
                table: "messages",
                column: "TransmitterId");

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_doctorId",
                table: "users",
                column: "doctorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENTS_users_UserId",
                table: "APPOINTMENTS");

            migrationBuilder.DropForeignKey(
                name: "FK_users_users_doctorId",
                table: "users");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "USERS");

            migrationBuilder.RenameIndex(
                name: "IX_users_doctorId",
                table: "USERS",
                newName: "IX_USERS_doctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS",
                table: "USERS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENTS_USERS_UserId",
                table: "APPOINTMENTS",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_USERS_doctorId",
                table: "USERS",
                column: "doctorId",
                principalTable: "USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
