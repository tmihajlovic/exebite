using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class OneToMenyRecipeRestoraunt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Recipe");
        }
    }
}
