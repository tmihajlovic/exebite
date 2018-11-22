using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class DateIndexAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_Date",
                table: "Order",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_Date",
                table: "Order");
        }
    }
}
