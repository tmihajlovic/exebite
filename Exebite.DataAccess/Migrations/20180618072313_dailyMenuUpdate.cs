using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class DailyMenuUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Food_FoodEntityId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantEntityId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_FoodEntityId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_RestaurantEntityId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "FoodEntityId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "RestaurantEntityId",
                table: "Food");

            migrationBuilder.CreateTable(
                name: "DailyMenuEntity",
                columns: table => new
                {
                    FoodEntityId = table.Column<int>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenuEntity", x => new { x.FoodEntityId, x.RestaurantId });
                    table.ForeignKey(
                        name: "FK_DailyMenuEntity_Food_FoodEntityId",
                        column: x => x.FoodEntityId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyMenuEntity_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyMenuEntity");

            migrationBuilder.AddColumn<int>(
                name: "FoodEntityId",
                table: "Food",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantEntityId",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_FoodEntityId",
                table: "Food",
                column: "FoodEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_RestaurantEntityId",
                table: "Food",
                column: "RestaurantEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Food_FoodEntityId",
                table: "Food",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantEntityId",
                table: "Food",
                column: "RestaurantEntityId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
