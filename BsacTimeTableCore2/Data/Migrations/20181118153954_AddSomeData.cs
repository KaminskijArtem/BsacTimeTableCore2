using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class AddSomeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
               SET IDENTITY_INSERT [Faculties]  ON;

               insert into [Faculties] (Id, Name) values
               (1, 'электросвязи'), 
               (2, 'инжиниринга и технологий связи'), 
               (3, 'заочного и дистанционного образования'), 
               (4, 'повышения квалификации и переподготовки кадров'), 
               (5, 'довузовской подготовки')

               SET IDENTITY_INSERT [Faculties]  OFF;

               insert into [AspNetRoles] (Id, ConcurrencyStamp, Name, NormalizedName) values
               (1, 'fdssfd', 'Admin', 'Admin')
             ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
                delete FROM [AspNetRoles]
                delete FROM [Faculties]
             ");
        }
    }
}
