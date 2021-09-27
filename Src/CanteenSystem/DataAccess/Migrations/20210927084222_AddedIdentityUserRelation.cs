using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedIdentityUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_UserId1",
                table: "UserAllergy",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId1",
                table: "UserAllergy",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId1",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_UserId1",
                table: "UserAllergy");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAllergy");
        }
    }
}
