using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Absence.DataAccess.Migrations
{
    public partial class Addedthemissingtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKWeekScheduleId",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
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
                name: "HourSchedules",
                columns: table => new
                {
                    HourScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKSubjectId = table.Column<int>(type: "int", nullable: false),
                    FKDayScheduleId = table.Column<int>(type: "int", nullable: false)
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
                    FKStudentId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceReports_FKHourScheduleId",
                table: "AbsenceReports",
                column: "FKHourScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedules_FKWeekScheduleId",
                table: "DaySchedules",
                column: "FKWeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_HourSchedules_FKDayScheduleId",
                table: "HourSchedules",
                column: "FKDayScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_HourSchedules_FKSubjectId",
                table: "HourSchedules",
                column: "FKSubjectId");

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
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "HourSchedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "DaySchedules");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "WeekSchedules");

            migrationBuilder.DropColumn(
                name: "FKWeekScheduleId",
                table: "Classrooms");
        }
    }
}
