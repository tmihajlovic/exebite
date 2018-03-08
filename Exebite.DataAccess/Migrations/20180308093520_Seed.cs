using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exebite.DataAccess.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed restaurants
            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new string[] { "Id", "Name" },
                values: new object[] { 1, "Restoran pod Lipom" });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new string[] { "Id", "Name" },
                values: new object[] { 2, "Hedone" });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new string[] { "Id", "Name" },
                values: new object[] { 3, "Index House" });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new string[] { "Id", "Name" },
                values: new object[] { 4, "Teglas" });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new string[] { "Id", "Name" },
                values: new object[] { 5, "Extra Food" });

            // Seed locations
            migrationBuilder.InsertData(
                table: "Location",
                columns: new string[] { "Id", "Name", "Address" },
                values: new object[] { 1, "Bulevar", "Bulevar Vojvode Stjepe 50" });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new string[] { "Id", "Name", "Address" },
                values: new object[] { 2, "JD", "Jovana Ducica" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
