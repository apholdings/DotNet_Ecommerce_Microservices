using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    ParentCategoryId = table.Column<int>(type: "integer", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    AverageRating = table.Column<double>(type: "double precision", nullable: false),
                    NumPurchases = table.Column<int>(type: "integer", nullable: false),
                    NumViews = table.Column<int>(type: "integer", nullable: false),
                    NumLikes = table.Column<int>(type: "integer", nullable: false),
                    AvgTimeSpent = table.Column<int>(type: "integer", nullable: false),
                    ClickThroughRate = table.Column<double>(type: "double precision", nullable: false),
                    ConversionRate = table.Column<double>(type: "double precision", nullable: false),
                    TotalRevenue = table.Column<double>(type: "double precision", nullable: false),
                    NumReturns = table.Column<int>(type: "integer", nullable: false),
                    NumRefunds = table.Column<int>(type: "integer", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    OnSale = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Videos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "Name", "ParentCategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5757), "Smart home devices and systems", "Smart Home", null, null },
                    { 2, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5760), "Electronic devices and gadgets", "Electronics", null, null },
                    { 3, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5761), "Arduino microcontroller boards and kits", "Arduino", 1, null },
                    { 4, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5762), "Electronic accessories and peripherals", "Accessories", 2, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "AvgTimeSpent", "CategoryId", "ClickThroughRate", "ConversionRate", "CreatedAt", "Description", "Manufacturer", "Name", "NumLikes", "NumPurchases", "NumRefunds", "NumReturns", "NumViews", "OnSale", "OwnerId", "Price", "Quantity", "Slug", "TotalRevenue" },
                values: new object[,]
                {
                    { 2, 4.0, 45, 1, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5898), "Its more than just a microcontroller!", "Raspberry Pi Foundation", "Raspberry Pi", 450, 182, 3, 0, 764, false, "username", 19.99m, 10, "raspberry-pi", 899.89999999999998 },
                    { 3, 4.0, 45, 2, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5936), "Its better than the xbox", "Sony Electornics", "PlayStation 5", 450, 182, 3, 0, 764, false, "username", 599.99m, 10, "playstation-5", 899.89999999999998 },
                    { 4, 4.0, 45, 2, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5962), "Anthenna for ethical hacking with kali linux", "Atheros", "Atheros 9271L", 450, 182, 3, 0, 764, false, "username", 19.99m, 10, "atheros-9271l", 899.89999999999998 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5903), "username", 2, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" },
                    { 3, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5943), "username", 3, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" },
                    { 4, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5968), "username", 4, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "AvgTimeSpent", "CategoryId", "ClickThroughRate", "ConversionRate", "CreatedAt", "Description", "Manufacturer", "Name", "NumLikes", "NumPurchases", "NumRefunds", "NumReturns", "NumViews", "OnSale", "OwnerId", "Price", "Quantity", "Slug", "TotalRevenue" },
                values: new object[] { 1, 4.5, 60, 3, 0.20000000000000001, 0.10000000000000001, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5870), "A microcontroller board based on the ATmega328 microcontroller.", "Arduino LLC", "Arduino Uno", 500, 100, 5, 10, 1000, false, "username", 29.99m, 10, "arduino-uno", 499.89999999999998 });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5909), "username", 2, "https://www.youtube.com/watch?v=jDigbTQ7xAM" },
                    { 3, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5954), "username", 3, "https://www.youtube.com/watch?v=jDigbTQ7xAM" },
                    { 4, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5973), "username", 4, "https://www.youtube.com/watch?v=jDigbTQ7xAM" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[] { 1, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5879), "username", 1, "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "VideoId", "CreatedAt", "OwnerId", "ProductId", "Url" },
                values: new object[] { 1, new DateTime(2022, 12, 26, 20, 56, 58, 617, DateTimeKind.Utc).AddTicks(5888), "username", 1, "https://www.youtube.com/watch?v=jDigbTQ7xAM" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductId",
                table: "Categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Description",
                table: "Products",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name_FTS",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ProductId",
                table: "Videos",
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

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
