using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BsacTimeTableCore2.Data.Migrations
{
    public partial class InitData : Migration
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

                insert into [AspNetUsers] ([Id]
              ,[AccessFailedCount]
              ,[ConcurrencyStamp]
              ,[Email]
              ,[EmailConfirmed]
              ,[LockoutEnabled]
              ,[LockoutEnd]
              ,[NormalizedEmail]
              ,[NormalizedUserName]
              ,[PasswordHash]
              ,[PhoneNumber]
              ,[PhoneNumberConfirmed]
              ,[SecurityStamp]
              ,[TwoFactorEnabled]
              ,[UserName])
	          values (
	          '808e2a85-ba89-4ec6-9738-de45ff50921b',0,	'c0a0a3ea-1266-44e9-86a6-37df98ce1351',
	  	        'test@test.test',0,	1,	NULL,	'TEST@TEST.TEST',	'TEST@TEST.TEST',	'AQAAAAEAACcQAAAAELShZMH7Gwf8A42vAFA7e1EhGQLlKR0pjIDBp+OvaoU7Iz3FtgjQlMwz8U/VNnLlDg==',	NULL,
			        0,	'c389da7c-4b14-45fe-9c09-7151df8fa47a',	0	,'test@test.test'),

			        ('d695bdfa-60c9-4c56-85e1-e06aedc10bf6',	0,	'c1ef6de4-aabc-42fc-a5d9-fced02cb841a',	'testAdmin@test.test',	0,	
			        1,	NULL,	'TESTADMIN@TEST.TEST',	'TESTADMIN@TEST.TEST','AQAAAAEAACcQAAAAEPWdWENQLPgkANa4JvKUZTb247TvOqtH/q0LNRmfOi7B/5V85sN/jHNALZWxmIgPBQ==',	NULL,     0,	'ccb8a189-bf0a-4176-ad07-394b7cba7adb',	0,	'testAdmin@test.test')
                
                insert into [AspNetUserRoles] values ('d695bdfa-60c9-4c56-85e1-e06aedc10bf6',1)

                  SET IDENTITY_INSERT [SubjectFor]  ON;

               insert into [SubjectFor] (Id, Name) values
               (1, '1я подгруппа'), 
               (2, '2я подгруппа'), 
               (3, 'для всех')

               SET IDENTITY_INSERT [SubjectFor]  OFF;

                SET IDENTITY_INSERT [SubjectTypes]  ON;

               insert into [SubjectTypes] (Id, Name) values
               (1, 'Лекция'), 
               (2, 'Лаба'), 
               (3, 'ПЗ'),
			   (4, 'Экзамен'),
			   (5, 'Консультация')

               SET IDENTITY_INSERT [SubjectTypes]  OFF;


               insert into [Subjects]
               values ('T', 1, 'TestSubj')


               insert into [Lecturers]
               values ( 'TestLecturer')


               insert into [Classrooms]
               values ( 1, '222')

                insert into [Groups]
               values ( 1, 'TestGroup')

             ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
