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
                name: "Classrooms",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FKSchoolId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "SchoolId", "Address", "Name" },
                values: new object[] { 1, "Hilmar Finsens Gade 18, 6400 Sønderborg", "EUC Syd" });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "SchoolId", "Address", "Name" },
                values: new object[] { 2, "Degnevænget 2, 6300 Gråsten", "Gråsten Skole" });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "FKSchoolId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Tokyo" },
                    { 3, 1, "Hongkong" },
                    { 2, 2, "Oslo" },
                    { 4, 2, "Paris" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_FKClassroomId",
                table: "Cameras",
                column: "FKClassroomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_FKSchoolId",
                table: "Classrooms",
                column: "FKSchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
