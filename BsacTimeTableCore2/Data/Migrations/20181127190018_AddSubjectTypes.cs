using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class AddSubjectTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_SubjectType_SubjectTypeId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectType",
                table: "SubjectType");

            migrationBuilder.RenameTable(
                name: "SubjectType",
                newName: "SubjectTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTypes",
                table: "SubjectTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_SubjectTypes_SubjectTypeId",
                table: "Records",
                column: "SubjectTypeId",
                principalTable: "SubjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_SubjectTypes_SubjectTypeId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTypes",
                table: "SubjectTypes");

            migrationBuilder.RenameTable(
                name: "SubjectTypes",
                newName: "SubjectType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectType",
                table: "SubjectType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_SubjectType_SubjectTypeId",
                table: "Records",
                column: "SubjectTypeId",
                principalTable: "SubjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
