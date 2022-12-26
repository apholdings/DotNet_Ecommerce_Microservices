using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIDfromVideoandImageDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(8931));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(8945));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(8947));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(8948));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9069));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9092));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9110));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9127));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9087));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9105));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9121));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9077));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9115));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 10, 21, 53, 198, DateTimeKind.Local).AddTicks(9154));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3797));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3808));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3809));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3811));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3929));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3991));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3971));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3987));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3941));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3964));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3981));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 26, 9, 37, 14, 130, DateTimeKind.Local).AddTicks(3996));
        }
    }
}
