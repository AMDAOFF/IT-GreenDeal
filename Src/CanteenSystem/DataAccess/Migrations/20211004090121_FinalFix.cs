using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FinalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Ingredient_IngredientId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId1",
                table: "UserAllergy");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAllergy",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_AllergyId1",
                table: "UserAllergy");

            migrationBuilder.DropColumn(
                name: "AllergyId1",
                table: "UserAllergy");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AllergyId",
                table: "UserAllergy",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "UserAllergyId",
                table: "UserAllergy",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Allergies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAllergy",
                table: "UserAllergy",
                column: "UserAllergyId");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DishId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_AllergyId",
                table: "UserAllergy",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_UserId",
                table: "UserAllergy",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ApplicationUserId",
                table: "Allergies",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_DishId",
                table: "Ingredients",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationUserId",
                table: "Allergies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Ingredients_IngredientId",
                table: "Allergies",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationUserId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Ingredients_IngredientId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId",
                table: "UserAllergy");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAllergy_AspNetUsers_UserId",
                table: "UserAllergy");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAllergy",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_AllergyId",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_UserAllergy_UserId",
                table: "UserAllergy");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_ApplicationUserId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "UserAllergyId",
                table: "UserAllergy");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Allergies");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAllergy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AllergyId",
                table: "UserAllergy",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AllergyId1",
                table: "UserAllergy",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAllergy",
                table: "UserAllergy",
                columns: new[] { "AllergyId", "UserId" });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishId = table.Column<int>(type: "int", nullable: true),
                    IngrediendId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredient_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAllergy_AllergyId1",
                table: "UserAllergy",
                column: "AllergyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_DishId",
                table: "Ingredient",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Ingredient_IngredientId",
                table: "Allergies",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAllergy_Allergies_AllergyId1",
                table: "UserAllergy",
                column: "AllergyId1",
                principalTable: "Allergies",
                principalColumn: "AllergyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
