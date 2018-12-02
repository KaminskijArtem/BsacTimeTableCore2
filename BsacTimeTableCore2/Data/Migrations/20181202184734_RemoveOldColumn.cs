using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class RemoveOldColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Records");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "Records",
                nullable: false,
                defaultValue: 0);
        }
    }
}
