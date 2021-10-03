using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UserList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Allergies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ApplicationUserId",
                table: "Allergies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationUserId",
                table: "Allergies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationUserId",
                table: "Allergies");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_ApplicationUserId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Allergies");
        }
    }
}
