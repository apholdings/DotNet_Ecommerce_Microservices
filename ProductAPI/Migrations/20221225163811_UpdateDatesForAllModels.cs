using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatesForAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Videos",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Videos",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Images",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Images",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Images");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 10, 43, 10, 525, DateTimeKind.Local).AddTicks(4580));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 10, 43, 10, 525, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 10, 43, 10, 525, DateTimeKind.Local).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2022, 12, 25, 10, 43, 10, 525, DateTimeKind.Local).AddTicks(4674));
        }
    }
}
