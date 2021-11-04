using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.DataAccess.Migrations
{
    public partial class seedDataToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "Id", "CityName", "Content", "CreationDate", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Cairo", "<div><h1>This Is Trip Journy To Cairo Content</h1></div>", new DateTime(2021, 10, 31, 12, 18, 49, 371, DateTimeKind.Local).AddTicks(336), "/images/img1.jpg", "Journy To Cairo", 5000m },
                    { 2, "Luxor", "<div><h1>This Is Trip Journy To Luxor Content</h1></div>", new DateTime(2021, 10, 31, 12, 18, 49, 371, DateTimeKind.Local).AddTicks(7716), "/images/img2.jpg", "Journy To Luxor", 10000m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "nadermostafa11@gmail.com", "12345678" },
                    { 2, "nadermostafa12@gmail.com", "12345678" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trip",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
