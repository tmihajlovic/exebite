using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class DailyMenuAddedInFoodEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuEntityId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "DailyMenuEntityId",
                table: "Food",
                newName: "DailyMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_DailyMenuEntityId",
                table: "Food",
                newName: "IX_Food_DailyMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food",
                column: "DailyMenuId",
                principalTable: "DailyMenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "DailyMenuId",
                table: "Food",
                newName: "DailyMenuEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Food_DailyMenuId",
                table: "Food",
                newName: "IX_Food_DailyMenuEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_DailyMenuEntity_DailyMenuEntityId",
                table: "Food",
                column: "DailyMenuEntityId",
                principalTable: "DailyMenuEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
