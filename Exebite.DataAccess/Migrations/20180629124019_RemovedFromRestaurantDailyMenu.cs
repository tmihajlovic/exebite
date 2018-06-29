using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class RemovedFromRestaurantDailyMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.DropColumn(
                name: "DailyMenuId",
                table: "Restaurant");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.AddColumn<int>(
                name: "DailyMenuId",
                table: "Restaurant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId",
                unique: true);
        }
    }
}
