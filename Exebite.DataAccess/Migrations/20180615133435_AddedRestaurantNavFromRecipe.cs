using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class AddedRestaurantNavFromRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantEntityId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Restaurant_RestaurantEntityId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RestaurantEntityId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Food_RestaurantEntityId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "RestaurantEntityId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RestaurantEntityId",
                table: "Food");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RestaurantId",
                table: "Recipe",
                column: "RestaurantId");

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
                name: "FK_Recipe_Restaurant_RestaurantId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RestaurantId",
                table: "Recipe");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantEntityId",
                table: "Recipe",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantEntityId",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RestaurantEntityId",
                table: "Recipe",
                column: "RestaurantEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_RestaurantEntityId",
                table: "Food",
                column: "RestaurantEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantEntityId",
                table: "Food",
                column: "RestaurantEntityId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Restaurant_RestaurantEntityId",
                table: "Recipe",
                column: "RestaurantEntityId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
