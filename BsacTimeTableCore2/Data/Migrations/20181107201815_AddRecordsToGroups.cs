using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class AddRecordsToGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Records_GroupId",
                table: "Records",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Groups_GroupId",
                table: "Records",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Groups_GroupId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_GroupId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Records");
        }
    }
}
