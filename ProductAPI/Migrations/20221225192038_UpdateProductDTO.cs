using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6297));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6311));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6313));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6314));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6461));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6519));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6456));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6476));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6446));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6486));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 14, 20, 38, 34, DateTimeKind.Local).AddTicks(6525));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8076));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8079));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8192));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8216));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8234));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8273));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8210));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8239));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8278));
        }
    }
}
