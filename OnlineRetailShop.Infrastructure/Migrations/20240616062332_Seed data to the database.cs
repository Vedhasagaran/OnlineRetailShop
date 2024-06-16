using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineRetailShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeddatatothedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("5ee17da1-281a-48de-a16d-b6d8d25c64bf"), "Orange", 200m, 200 },
                    { new Guid("f974bc9e-fbee-4704-baf4-0c175ba56381"), "Appple", 1200m, 100 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("5214396e-c2f3-437d-8e00-500dc2a8734a"), new DateTime(2024, 6, 16, 11, 53, 27, 760, DateTimeKind.Local).AddTicks(6726), new Guid("f974bc9e-fbee-4704-baf4-0c175ba56381"), 10 },
                    { new Guid("78d43038-ca44-420c-9361-6f9e2d292c18"), new DateTime(2024, 6, 16, 11, 53, 27, 760, DateTimeKind.Local).AddTicks(6740), new Guid("5ee17da1-281a-48de-a16d-b6d8d25c64bf"), 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5214396e-c2f3-437d-8e00-500dc2a8734a"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("78d43038-ca44-420c-9361-6f9e2d292c18"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5ee17da1-281a-48de-a16d-b6d8d25c64bf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f974bc9e-fbee-4704-baf4-0c175ba56381"));
        }
    }
}
