using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(6915));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(6917));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(6919));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(6938));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7055));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7096));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7115));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DiscountPrice" },
                values: new object[] { new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7048), 0m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DiscountPrice" },
                values: new object[] { new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7073), 0m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DiscountPrice" },
                values: new object[] { new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7091), 0m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DiscountPrice" },
                values: new object[] { new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7110), 0m });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7063));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7103));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 21, 6, 39, 715, DateTimeKind.Utc).AddTicks(7119));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5757));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5762));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5879));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5943));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5968));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5898));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5936));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5962));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5909));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5954));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5973));
        }
    }
}
