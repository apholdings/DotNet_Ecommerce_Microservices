using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductCategoriesList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ProductId" },
                values: new object[] { new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8064), null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ProductId" },
                values: new object[] { new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8076), null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ProductId" },
                values: new object[] { new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8077), null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ProductId" },
                values: new object[] { new DateTime(2022, 12, 25, 12, 18, 2, 940, DateTimeKind.Local).AddTicks(8079), null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductId",
                table: "Categories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductId",
                table: "Categories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8574));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8585));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8586));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8588));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8714));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8780));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8796));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8704));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8775));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8724));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8768));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8785));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "VideoId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8801));
        }
    }
}
