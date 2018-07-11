using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAliasesEntities_Customer_CustomerId",
                table: "CustomerAliasesEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAliasesEntities_Restaurant_RestaurantId",
                table: "CustomerAliasesEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyMenuEntity_Restaurant_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntityMealEntities_Food_FoodEntityId",
                table: "FoodEntityMealEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntityMealEntities_Meal_MealEntityId",
                table: "FoodEntityMealEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntityRecipeEntity_Food_FoodEntityId",
                table: "FoodEntityRecipeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntityRecipeEntity_Recipe_RecepieEntityId",
                table: "FoodEntityRecipeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodEntityRecipeEntity",
                table: "FoodEntityRecipeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodEntityMealEntities",
                table: "FoodEntityMealEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyMenuEntity",
                table: "DailyMenuEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAliasesEntities",
                table: "CustomerAliasesEntities");

            migrationBuilder.RenameTable(
                name: "FoodEntityRecipeEntity",
                newName: "FoodToRecipe");

            migrationBuilder.RenameTable(
                name: "FoodEntityMealEntities",
                newName: "FoodToMeal");

            migrationBuilder.RenameTable(
                name: "DailyMenuEntity",
                newName: "DailyMenu");

            migrationBuilder.RenameTable(
                name: "CustomerAliasesEntities",
                newName: "CustomerAliases");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Customer",
                newName: "GoogleUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodEntityRecipeEntity_RecepieEntityId",
                table: "FoodToRecipe",
                newName: "IX_FoodToRecipe_RecepieEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodEntityMealEntities_MealEntityId",
                table: "FoodToMeal",
                newName: "IX_FoodToMeal_MealEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenu",
                newName: "IX_DailyMenu_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAliasesEntities_RestaurantId",
                table: "CustomerAliases",
                newName: "IX_CustomerAliases_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAliasesEntities_CustomerId",
                table: "CustomerAliases",
                newName: "IX_CustomerAliases_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodToRecipe",
                table: "FoodToRecipe",
                columns: new[] { "FoodEntityId", "RecepieEntityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodToMeal",
                table: "FoodToMeal",
                columns: new[] { "FoodEntityId", "MealEntityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyMenu",
                table: "DailyMenu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAliases",
                table: "CustomerAliases",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RoleId",
                table: "Customer",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Role_RoleId",
                table: "Customer",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAliases_Customer_CustomerId",
                table: "CustomerAliases",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAliases_Restaurant_RestaurantId",
                table: "CustomerAliases",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyMenu_Restaurant_RestaurantId",
                table: "DailyMenu",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_DailyMenu_DailyMenuId",
                table: "Food",
                column: "DailyMenuId",
                principalTable: "DailyMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodToMeal_Food_FoodEntityId",
                table: "FoodToMeal",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodToMeal_Meal_MealEntityId",
                table: "FoodToMeal",
                column: "MealEntityId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodToRecipe_Food_FoodEntityId",
                table: "FoodToRecipe",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodToRecipe_Recipe_RecepieEntityId",
                table: "FoodToRecipe",
                column: "RecepieEntityId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Role_RoleId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAliases_Customer_CustomerId",
                table: "CustomerAliases");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAliases_Restaurant_RestaurantId",
                table: "CustomerAliases");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyMenu_Restaurant_RestaurantId",
                table: "DailyMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenu_DailyMenuId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodToMeal_Food_FoodEntityId",
                table: "FoodToMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodToMeal_Meal_MealEntityId",
                table: "FoodToMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodToRecipe_Food_FoodEntityId",
                table: "FoodToRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodToRecipe_Recipe_RecepieEntityId",
                table: "FoodToRecipe");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Customer_RoleId",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodToRecipe",
                table: "FoodToRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodToMeal",
                table: "FoodToMeal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyMenu",
                table: "DailyMenu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAliases",
                table: "CustomerAliases");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "FoodToRecipe",
                newName: "FoodEntityRecipeEntity");

            migrationBuilder.RenameTable(
                name: "FoodToMeal",
                newName: "FoodEntityMealEntities");

            migrationBuilder.RenameTable(
                name: "DailyMenu",
                newName: "DailyMenuEntity");

            migrationBuilder.RenameTable(
                name: "CustomerAliases",
                newName: "CustomerAliasesEntities");

            migrationBuilder.RenameColumn(
                name: "GoogleUserId",
                table: "Customer",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodToRecipe_RecepieEntityId",
                table: "FoodEntityRecipeEntity",
                newName: "IX_FoodEntityRecipeEntity_RecepieEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodToMeal_MealEntityId",
                table: "FoodEntityMealEntities",
                newName: "IX_FoodEntityMealEntities_MealEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyMenu_RestaurantId",
                table: "DailyMenuEntity",
                newName: "IX_DailyMenuEntity_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAliases_RestaurantId",
                table: "CustomerAliasesEntities",
                newName: "IX_CustomerAliasesEntities_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAliases_CustomerId",
                table: "CustomerAliasesEntities",
                newName: "IX_CustomerAliasesEntities_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodEntityRecipeEntity",
                table: "FoodEntityRecipeEntity",
                columns: new[] { "FoodEntityId", "RecepieEntityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodEntityMealEntities",
                table: "FoodEntityMealEntities",
                columns: new[] { "FoodEntityId", "MealEntityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyMenuEntity",
                table: "DailyMenuEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAliasesEntities",
                table: "CustomerAliasesEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAliasesEntities_Customer_CustomerId",
                table: "CustomerAliasesEntities",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAliasesEntities_Restaurant_RestaurantId",
                table: "CustomerAliasesEntities",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyMenuEntity_Restaurant_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food",
                column: "DailyMenuId",
                principalTable: "DailyMenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntityMealEntities_Food_FoodEntityId",
                table: "FoodEntityMealEntities",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntityMealEntities_Meal_MealEntityId",
                table: "FoodEntityMealEntities",
                column: "MealEntityId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntityRecipeEntity_Food_FoodEntityId",
                table: "FoodEntityRecipeEntity",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntityRecipeEntity_Recipe_RecepieEntityId",
                table: "FoodEntityRecipeEntity",
                column: "RecepieEntityId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
