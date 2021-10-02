using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absence.DataAccess.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FKSchoolId = table.Column<int>(type: "int", nullable: false),
                    FKWeekScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ClassroomId);
                    table.ForeignKey(
                        name: "FK_Classrooms_Schools_FKSchoolId",
                        column: x => x.FKSchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FKSchoolId = table.Column<int>(type: "int", nullable: false),
                    FKSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teachers_Schools_FKSchoolId",
                        column: x => x.FKSchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Subjects_FKSubjectId",
                        column: x => x.FKSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    IP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FKClassroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.IP);
                    table.ForeignKey(
                        name: "FK_Cameras_Classrooms_FKClassroomId",
                        column: x => x.FKClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekSchedules",
                columns: table => new
                {
                    WeekScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKClassroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekSchedules", x => x.WeekScheduleId);
                    table.ForeignKey(
                        name: "FK_WeekSchedules_Classrooms_FKClassroomId",
                        column: x => x.FKClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentClasses",
                columns: table => new
                {
                    StudentClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FKTeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => x.StudentClassId);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Teachers_FKTeacherId",
                        column: x => x.FKTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaySchedules",
                columns: table => new
                {
                    DayScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKWeekScheduleId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchedules", x => x.DayScheduleId);
                    table.ForeignKey(
                        name: "FK_DaySchedules_WeekSchedules_FKWeekScheduleId",
                        column: x => x.FKWeekScheduleId,
                        principalTable: "WeekSchedules",
                        principalColumn: "WeekScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FKStudentClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_StudentClasses_FKStudentClassId",
                        column: x => x.FKStudentClassId,
                        principalTable: "StudentClasses",
                        principalColumn: "StudentClassId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HourSchedules",
                columns: table => new
                {
                    HourScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKSubjectId = table.Column<int>(type: "int", nullable: false),
                    FKDayScheduleId = table.Column<int>(type: "int", nullable: false),
                    FKStudentClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourSchedules", x => x.HourScheduleId);
                    table.ForeignKey(
                        name: "FK_HourSchedules_DaySchedules_FKDayScheduleId",
                        column: x => x.FKDayScheduleId,
                        principalTable: "DaySchedules",
                        principalColumn: "DayScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourSchedules_StudentClasses_FKStudentClassId",
                        column: x => x.FKStudentClassId,
                        principalTable: "StudentClasses",
                        principalColumn: "StudentClassId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HourSchedules_Subjects_FKSubjectId",
                        column: x => x.FKSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceReports",
                columns: table => new
                {
                    FKStudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FKHourScheduleId = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceReports", x => new { x.FKStudentId, x.FKHourScheduleId });
                    table.ForeignKey(
                        name: "FK_AbsenceReports_HourSchedules_FKHourScheduleId",
                        column: x => x.FKHourScheduleId,
                        principalTable: "HourSchedules",
                        principalColumn: "HourScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbsenceReports_Students_FKStudentId",
                        column: x => x.FKStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "SchoolId", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Hilmar Finsens Gade 18, 6400 Sønderborg", "EUC Syd" },
                    { 2, "Degnevænget 2, 6300 Gråsten", "Gråsten Skole" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "Name" },
                values: new object[,]
                {
                    { 1, "¨Programmering" },
                    { 2, "Netværk" },
                    { 3, "Engelsk" },
                    { 4, "Dansk" },
                    { 5, "Idræt" },
                    { 6, "Matematik" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "ClassroomNumber", "FKSchoolId", "FKWeekScheduleId", "Name" },
                values: new object[,]
                {
                    { 1, "52.211", 1, 0, "Tokyo" },
                    { 3, "51.157", 1, 0, "Hongkong" },
                    { 2, "52.212", 2, 0, "Oslo" },
                    { 4, "51.131", 2, 0, "Paris" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "FKSchoolId", "FKSubjectId", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "Egon Christian Rasmussen" },
                    { 2, 1, 2, "Tina Hansen" },
                    { 3, 2, 4, "Hans Jørgen Petersen" },
                    { 4, 2, 6, "Flemming Nielsen" }
                });

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "IP", "FKClassroomId" },
                values: new object[,]
                {
                    { "192.168.0.1", 1 },
                    { "192.168.0.2", 3 },
                    { "192.168.1.1", 2 },
                    { "192.168.1.2", 4 }
                });

            migrationBuilder.InsertData(
                table: "StudentClasses",
                columns: new[] { "StudentClassId", "FKTeacherId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "H6 Programmører" },
                    { 2, 2, "H2 Infrastruktur" },
                    { 3, 3, "8 Klasse" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FKStudentClassId", "Name" },
                values: new object[,]
                {
                    { "jimm1576", 1, "Jimmy Elkjer" },
                    { "jona153m", 1, "Jonas Peter Fuhlendorff Jørgensen" },
                    { "kenn8174", 1, "Kenneth Jessen" },
                    { "kris593d", 1, "Kristian Biehl Kuhrt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceReports_FKHourScheduleId",
                table: "AbsenceReports",
                column: "FKHourScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_FKClassroomId",
                table: "Cameras",
                column: "FKClassroomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_FKSchoolId",
                table: "Classrooms",
                column: "FKSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedules_FKWeekScheduleId",
                table: "DaySchedules",
                column: "FKWeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_HourSchedules_FKDayScheduleId",
                table: "HourSchedules",
                column: "FKDayScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_HourSchedules_FKStudentClassId",
                table: "HourSchedules",
                column: "FKStudentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_HourSchedules_FKSubjectId",
                table: "HourSchedules",
                column: "FKSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_FKTeacherId",
                table: "StudentClasses",
                column: "FKTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FKStudentClassId",
                table: "Students",
                column: "FKStudentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_FKSchoolId",
                table: "Teachers",
                column: "FKSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_FKSubjectId",
                table: "Teachers",
                column: "FKSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekSchedules_FKClassroomId",
                table: "WeekSchedules",
                column: "FKClassroomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceReports");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "HourSchedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "DaySchedules");

            migrationBuilder.DropTable(
                name: "StudentClasses");

            migrationBuilder.DropTable(
                name: "WeekSchedules");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
