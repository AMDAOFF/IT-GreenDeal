using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAllergies_Allergies_IngredientId",
                table: "IngredientAllergies");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAllergies_Ingredients_IngredientId1",
                table: "IngredientAllergies");

            migrationBuilder.DropIndex(
                name: "IX_IngredientAllergies_IngredientId1",
                table: "IngredientAllergies");

            migrationBuilder.DropColumn(
                name: "IngredientId1",
                table: "IngredientAllergies");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAllergies_AllergyId",
                table: "IngredientAllergies",
                column: "AllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAllergies_Allergies_AllergyId",
                table: "IngredientAllergies",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAllergies_Ingredients_IngredientId",
                table: "IngredientAllergies",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAllergies_Allergies_AllergyId",
                table: "IngredientAllergies");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAllergies_Ingredients_IngredientId",
                table: "IngredientAllergies");

            migrationBuilder.DropIndex(
                name: "IX_IngredientAllergies_AllergyId",
                table: "IngredientAllergies");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId1",
                table: "IngredientAllergies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAllergies_IngredientId1",
                table: "IngredientAllergies",
                column: "IngredientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAllergies_Allergies_IngredientId",
                table: "IngredientAllergies",
                column: "IngredientId",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAllergies_Ingredients_IngredientId1",
                table: "IngredientAllergies",
                column: "IngredientId1",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
