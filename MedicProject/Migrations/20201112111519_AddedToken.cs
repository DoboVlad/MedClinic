﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicProject.Migrations
{
    public partial class AddedToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "USERS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "USERS");
        }
    }
}
