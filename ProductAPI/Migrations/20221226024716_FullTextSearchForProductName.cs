﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class FullTextSearchForProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.RenameIndex(
                name: "IX_Product_Name",
                table: "Products",
                newName: "IX_Product_Name_FTS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Product_Name_FTS",
                table: "Products",
                newName: "IX_Product_Name");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "Name", "ParentCategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9481), "Smart home devices and systems", "Smart Home", null, null },
                    { 2, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9493), "Electronic devices and gadgets", "Electronics", null, null },
                    { 3, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9495), "Arduino microcontroller boards and kits", "Arduino", 1, null },
                    { 4, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9496), "Electronic accessories and peripherals", "Accessories", 2, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "AvgTimeSpent", "CategoryId", "ClickThroughRate", "ConversionRate", "CreatedAt", "Description", "Manufacturer", "Name", "NumLikes", "NumPurchases", "NumRefunds", "NumReturns", "NumViews", "OnSale", "OwnerId", "Price", "Quantity", "TotalRevenue" },
                values: new object[,]
                {
                    { 2, 4.0, 45, 1, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9657), "Its more than just a microcontroller!", "Raspberry Pi Foundation", "Raspberry Pi", 450, 182, 3, 0, 764, false, "username", 19.99m, 10, 899.89999999999998 },
                    { 3, 4.0, 45, 2, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9676), "Its better than the xbox", "Sony Electornics", "PlayStation 5", 450, 182, 3, 0, 764, false, "username", 599.99m, 10, 899.89999999999998 },
                    { 4, 4.0, 45, 2, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9693), "Anthenna for ethical hacking with kali linux", "Atheros", "Atheros 9271L", 450, 182, 3, 0, 764, false, "username", 19.99m, 10, 899.89999999999998 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9663), "username", 2, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" },
                    { 3, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9682), "username", 3, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" },
                    { 4, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9698), "username", 4, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "AvgTimeSpent", "CategoryId", "ClickThroughRate", "ConversionRate", "CreatedAt", "Description", "Manufacturer", "Name", "NumLikes", "NumPurchases", "NumRefunds", "NumReturns", "NumViews", "OnSale", "OwnerId", "Price", "Quantity", "TotalRevenue" },
                values: new object[] { 1, 4.5, 60, 3, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9608), "A microcontroller board based on the ATmega328 microcontroller.", "Arduino LLC", "Arduino Uno", 500, 100, 5, 10, 1000, false, "username", 29.99m, 10, 499.89999999999998 });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9669), "username", 2, "https://www.youtube.com/watch?v=jDigbTQ7xAM" },
                    { 3, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9687), "username", 3, "https://www.youtube.com/watch?v=jDigbTQ7xAM" },
                    { 4, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9703), "username", 4, "https://www.youtube.com/watch?v=jDigbTQ7xAM" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[] { 1, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9618), "username", 1, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[] { 1, new DateTime(2022, 12, 25, 19, 50, 23, 598, DateTimeKind.Local).AddTicks(9626), "username", 1, "https://www.youtube.com/watch?v=jDigbTQ7xAM" });
        }
    }
}