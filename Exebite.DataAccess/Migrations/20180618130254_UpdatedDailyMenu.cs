using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class UpdatedDailyMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyMenuEntity_Food_FoodEntityId",
                table: "DailyMenuEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyMenuEntity_Restaurant_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyMenuEntity",
                table: "DailyMenuEntity");

            migrationBuilder.DropIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.DropColumn(
                name: "FoodEntityId",
                table: "DailyMenuEntity");

            migrationBuilder.AddColumn<int>(
                name: "DailyMenuId",
                table: "Restaurant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DailyMenuId",
                table: "Food",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DailyMenuEntity",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyMenuEntity",
                table: "DailyMenuEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Food_DailyMenuId",
                table: "Food",
                column: "DailyMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId",
                unique: true);

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyMenuEntity_Restaurant_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_DailyMenuId",
                table: "Food");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyMenuEntity",
                table: "DailyMenuEntity");

            migrationBuilder.DropIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity");

            migrationBuilder.DropColumn(
                name: "DailyMenuId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "DailyMenuId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DailyMenuEntity");

            migrationBuilder.AddColumn<int>(
                name: "FoodEntityId",
                table: "DailyMenuEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyMenuEntity",
                table: "DailyMenuEntity",
                columns: new[] { "FoodEntityId", "RestaurantId" });

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenuEntity_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyMenuEntity_Food_FoodEntityId",
                table: "DailyMenuEntity",
                column: "FoodEntityId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyMenuEntity_Restaurant_RestaurantId",
                table: "DailyMenuEntity",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
