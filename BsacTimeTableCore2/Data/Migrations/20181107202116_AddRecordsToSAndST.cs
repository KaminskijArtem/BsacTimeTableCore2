using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class AddRecordsToSAndST : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectForId",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectTypeId",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubjectFor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectFor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_SubjectForId",
                table: "Records",
                column: "SubjectForId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_SubjectTypeId",
                table: "Records",
                column: "SubjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_SubjectFor_SubjectForId",
                table: "Records",
                column: "SubjectForId",
                principalTable: "SubjectFor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_SubjectType_SubjectTypeId",
                table: "Records",
                column: "SubjectTypeId",
                principalTable: "SubjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_SubjectFor_SubjectForId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_SubjectType_SubjectTypeId",
                table: "Records");

            migrationBuilder.DropTable(
                name: "SubjectFor");

            migrationBuilder.DropTable(
                name: "SubjectType");

            migrationBuilder.DropIndex(
                name: "IX_Records_SubjectForId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_SubjectTypeId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SubjectForId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "SubjectTypeId",
                table: "Records");
        }
    }
}
