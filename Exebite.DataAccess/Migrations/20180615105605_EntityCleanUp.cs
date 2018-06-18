using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class EntityCleanUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntityRecipeEntity_Food_FoodEntityId",
                table: "FoodEntityRecipeEntity");

            migrationBuilder.DropIndex(
                name: "IX_Order_MealId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RestaurantId",
                table: "Recipe",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MealId",
                table: "Order",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                table: "Food",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntityRecipeEntity_Food_FoodEntityId",
                table: "FoodEntityRecipeEntity",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Restaurant_RestaurantId",
                table: "Recipe",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntityRecipeEntity_Food_FoodEntityId",
                table: "FoodEntityRecipeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Restaurant_RestaurantId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RestaurantId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Order_MealId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Recipe");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MealId",
                table: "Order",
                column: "MealId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                table: "Food",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntityRecipeEntity_Food_FoodEntityId",
                table: "FoodEntityRecipeEntity",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
