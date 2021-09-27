using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updatedrealationinuserallergies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Ingredient_IngredientId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Dishes_DishId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId1",
                table: "UserAllergy");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId1",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_AllergyId1",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_UserId1",
                table: "UserAllergy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "AllergyId1",
                table: "UserAllergy");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAllergy");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_DishId",
                table: "Ingredients",
                newName: "IX_Ingredients_DishId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AllergyId",
                table: "UserAllergy",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_UserId",
                table: "UserAllergy",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Ingredients_IngredientId",
                table: "Allergies",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Dishes_DishId",
                table: "Ingredients",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "DishId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId",
                table: "UserAllergy",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId",
                table: "UserAllergy",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Ingredients_IngredientId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Dishes_DishId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId",
                table: "UserAllergy");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_UserId",
                table: "UserAllergy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_DishId",
                table: "Ingredient",
                newName: "IX_Ingredient_DishId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAllergy",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AllergyId",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AllergyId1",
                table: "UserAllergy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_AllergyId1",
                table: "UserAllergy",
                column: "AllergyId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_UserId1",
                table: "UserAllergy",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Ingredient_IngredientId",
                table: "Allergies",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Dishes_DishId",
                table: "Ingredient",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "DishId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId1",
                table: "UserAllergy",
                column: "AllergyId1",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId1",
                table: "UserAllergy",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
