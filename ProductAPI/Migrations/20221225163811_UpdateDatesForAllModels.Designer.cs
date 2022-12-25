﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProductAPI.Data;

#nullable disable

namespace ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221225163811_UpdateDatesForAllModels")]
    partial class UpdateDatesForAllModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProductAPI.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8574),
                            Description = "Smart home devices and systems",
                            Name = "Smart Home",
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8583)
                        },
                        new
                        {
                            CategoryId = 2,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8585),
                            Description = "Electronic devices and gadgets",
                            Name = "Electronics",
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8585)
                        },
                        new
                        {
                            CategoryId = 3,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8586),
                            Description = "Arduino microcontroller boards and kits",
                            Name = "Arduino",
                            ParentCategoryId = 1,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8587)
                        },
                        new
                        {
                            CategoryId = 4,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8588),
                            Description = "Electronic accessories and peripherals",
                            Name = "Accessories",
                            ParentCategoryId = 2,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8588)
                        });
                });

            modelBuilder.Entity("ProductAPI.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ImageId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8714),
                            OwnerId = "username",
                            ProductId = 1,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8715),
                            Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg"
                        },
                        new
                        {
                            ImageId = 2,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8761),
                            OwnerId = "username",
                            ProductId = 2,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8762),
                            Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg"
                        },
                        new
                        {
                            ImageId = 3,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8780),
                            OwnerId = "username",
                            ProductId = 3,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8780),
                            Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg"
                        },
                        new
                        {
                            ImageId = 4,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8796),
                            OwnerId = "username",
                            ProductId = 4,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8797),
                            Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg"
                        });
                });

            modelBuilder.Entity("ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<double>("AverageRating")
                        .HasColumnType("double precision");

                    b.Property<int>("AvgTimeSpent")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<double>("ClickThroughRate")
                        .HasColumnType("double precision");

                    b.Property<double>("ConversionRate")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("NumLikes")
                        .HasColumnType("integer");

                    b.Property<int>("NumPurchases")
                        .HasColumnType("integer");

                    b.Property<int>("NumRefunds")
                        .HasColumnType("integer");

                    b.Property<int>("NumReturns")
                        .HasColumnType("integer");

                    b.Property<int>("NumViews")
                        .HasColumnType("integer");

                    b.Property<bool>("OnSale")
                        .HasColumnType("boolean");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<double>("TotalRevenue")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            AverageRating = 4.5,
                            AvgTimeSpent = 60,
                            CategoryId = 3,
                            ClickThroughRate = 0.20000000000000001,
                            ConversionRate = 0.10000000000000001,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8704),
                            Description = "A microcontroller board based on the ATmega328 microcontroller.",
                            Manufacturer = "Arduino LLC",
                            Name = "Arduino Uno",
                            NumLikes = 500,
                            NumPurchases = 100,
                            NumRefunds = 5,
                            NumReturns = 10,
                            NumViews = 1000,
                            OnSale = false,
                            OwnerId = "username",
                            Price = 29.99m,
                            Quantity = 10,
                            TotalRevenue = 499.89999999999998,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8704)
                        },
                        new
                        {
                            ProductId = 2,
                            AverageRating = 4.0,
                            AvgTimeSpent = 45,
                            CategoryId = 1,
                            ClickThroughRate = 0.20000000000000001,
                            ConversionRate = 0.10000000000000001,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8755),
                            Description = "Its more than just a microcontroller!",
                            Manufacturer = "Raspberry Pi Foundation",
                            Name = "Raspberry Pi",
                            NumLikes = 450,
                            NumPurchases = 182,
                            NumRefunds = 3,
                            NumReturns = 0,
                            NumViews = 764,
                            OnSale = false,
                            OwnerId = "username",
                            Price = 19.99m,
                            Quantity = 10,
                            TotalRevenue = 899.89999999999998,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8755)
                        },
                        new
                        {
                            ProductId = 3,
                            AverageRating = 4.0,
                            AvgTimeSpent = 45,
                            CategoryId = 2,
                            ClickThroughRate = 0.20000000000000001,
                            ConversionRate = 0.10000000000000001,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8775),
                            Description = "Its better than the xbox",
                            Manufacturer = "Sony Electornics",
                            Name = "PlayStation 5",
                            NumLikes = 450,
                            NumPurchases = 182,
                            NumRefunds = 3,
                            NumReturns = 0,
                            NumViews = 764,
                            OnSale = false,
                            OwnerId = "username",
                            Price = 599.99m,
                            Quantity = 10,
                            TotalRevenue = 899.89999999999998,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8775)
                        },
                        new
                        {
                            ProductId = 4,
                            AverageRating = 4.0,
                            AvgTimeSpent = 45,
                            CategoryId = 2,
                            ClickThroughRate = 0.20000000000000001,
                            ConversionRate = 0.10000000000000001,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8792),
                            Description = "Anthenna for ethical hacking with kali linux",
                            Manufacturer = "Atheros",
                            Name = "Atheros 9271L",
                            NumLikes = 450,
                            NumPurchases = 182,
                            NumRefunds = 3,
                            NumReturns = 0,
                            NumViews = 764,
                            OnSale = false,
                            OwnerId = "username",
                            Price = 19.99m,
                            Quantity = 10,
                            TotalRevenue = 899.89999999999998,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8792)
                        });
                });

            modelBuilder.Entity("ProductAPI.Models.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VideoId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VideoId");

                    b.HasIndex("ProductId");

                    b.ToTable("Videos");

                    b.HasData(
                        new
                        {
                            VideoId = 1,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8724),
                            OwnerId = "username",
                            ProductId = 1,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8724),
                            Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM"
                        },
                        new
                        {
                            VideoId = 2,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8768),
                            OwnerId = "username",
                            ProductId = 2,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8768),
                            Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM"
                        },
                        new
                        {
                            VideoId = 3,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8785),
                            OwnerId = "username",
                            ProductId = 3,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8785),
                            Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM"
                        },
                        new
                        {
                            VideoId = 4,
                            CreatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8801),
                            OwnerId = "username",
                            ProductId = 4,
                            UpdatedAt = new DateTime(2022, 12, 25, 11, 38, 11, 289, DateTimeKind.Local).AddTicks(8802),
                            Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM"
                        });
                });

            modelBuilder.Entity("ProductAPI.Models.Category", b =>
                {
                    b.HasOne("ProductAPI.Models.Category", "ParentCategory")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("ProductAPI.Models.Image", b =>
                {
                    b.HasOne("ProductAPI.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductAPI.Models.Product", b =>
                {
                    b.HasOne("ProductAPI.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProductAPI.Models.Video", b =>
                {
                    b.HasOne("ProductAPI.Models.Product", "Product")
                        .WithMany("Videos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductAPI.Models.Category", b =>
                {
                    b.Navigation("ChildCategories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductAPI.Models.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
