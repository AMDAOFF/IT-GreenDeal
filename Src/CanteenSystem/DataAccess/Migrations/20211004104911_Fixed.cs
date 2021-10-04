using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId",
                table: "UserAllergy");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId",
                table: "UserAllergy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAllergy",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_AllergyId",
                table: "UserAllergy");

            migrationBuilder.DropColumn(
                name: "UserAllergyId",
                table: "UserAllergy");

            migrationBuilder.RenameTable(
                name: "UserAllergy",
                newName: "UserAllergies");

            migrationBuilder.RenameIndex(
                name: "IX_UserAllergy_UserId",
                table: "UserAllergies",
                newName: "IX_UserAllergies_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAllergies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AllergyId",
                table: "UserAllergies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAllergies",
                table: "UserAllergies",
                columns: new[] { "AllergyId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergies_Allergies_AllergyId",
                table: "UserAllergies",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergies_AspNetUsers_UserId",
                table: "UserAllergies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergies_Allergies_AllergyId",
                table: "UserAllergies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergies_AspNetUsers_UserId",
                table: "UserAllergies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAllergies",
                table: "UserAllergies");

            migrationBuilder.RenameTable(
                name: "UserAllergies",
                newName: "UserAllergy");

            migrationBuilder.RenameIndex(
                name: "IX_UserAllergies_UserId",
                table: "UserAllergy",
                newName: "IX_UserAllergy_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "AllergyId",
                table: "UserAllergy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserAllergyId",
                table: "UserAllergy",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAllergy",
                table: "UserAllergy",
                column: "UserAllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_AllergyId",
                table: "UserAllergy",
                column: "AllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId",
                table: "UserAllergy",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId",
                table: "UserAllergy",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
