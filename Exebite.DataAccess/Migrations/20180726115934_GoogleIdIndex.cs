using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class GoogleIdIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleUserId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GoogleUserId",
                table: "Customer",
                column: "GoogleUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_GoogleUserId",
                table: "Customer");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Payment",
                nullable: true,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleUserId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
