using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class UpdatedDailyMenu1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_DailyMenuId",
                table: "Food");

            migrationBuilder.AddColumn<int>(
                name: "DailyMenuEntityId",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_DailyMenuEntityId",
                table: "Food",
                column: "DailyMenuEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuEntityId",
                table: "Food",
                column: "DailyMenuEntityId",
                principalTable: "DailyMenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuEntityId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_DailyMenuEntityId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "DailyMenuEntityId",
                table: "Food");

            migrationBuilder.CreateIndex(
                name: "IX_Food_DailyMenuId",
                table: "Food",
                column: "DailyMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food",
                column: "DailyMenuId",
                principalTable: "DailyMenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
