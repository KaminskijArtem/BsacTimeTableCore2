using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class AddLecturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Records_LecturerId",
                table: "Records",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Lecturers_LecturerId",
                table: "Records",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Lecturers_LecturerId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_LecturerId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Records");
        }
    }
}
