using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class RemovedRestaurantNavFromRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RestaurantEntityId",
                table: "Recipe",
                column: "RestaurantEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Restaurant_RestaurantEntityId",
                table: "Recipe",
                column: "RestaurantEntityId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Restaurant_RestaurantEntityId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RestaurantEntityId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RestaurantEntityId",
                table: "Recipe");

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
    }
}
