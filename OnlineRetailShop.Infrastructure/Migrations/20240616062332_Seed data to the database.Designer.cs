﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineRetailShop.Infrastructure.Data;

#nullable disable

namespace OnlineRetailShop.Infrastructure.Migrations
{
    [DbContext(typeof(OnlineRetailShopDbContext))]
    [Migration("20240616062332_Seed data to the database")]
    partial class Seeddatatothedatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineRetailShop.Domain.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5214396e-c2f3-437d-8e00-500dc2a8734a"),
                            OrderDate = new DateTime(2024, 6, 16, 11, 53, 27, 760, DateTimeKind.Local).AddTicks(6726),
                            ProductId = new Guid("f974bc9e-fbee-4704-baf4-0c175ba56381"),
                            Quantity = 10
                        },
                        new
                        {
                            Id = new Guid("78d43038-ca44-420c-9361-6f9e2d292c18"),
                            OrderDate = new DateTime(2024, 6, 16, 11, 53, 27, 760, DateTimeKind.Local).AddTicks(6740),
                            ProductId = new Guid("5ee17da1-281a-48de-a16d-b6d8d25c64bf"),
                            Quantity = 20
                        });
                });

            modelBuilder.Entity("OnlineRetailShop.Domain.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f974bc9e-fbee-4704-baf4-0c175ba56381"),
                            Name = "Appple",
                            Price = 1200m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = new Guid("5ee17da1-281a-48de-a16d-b6d8d25c64bf"),
                            Name = "Orange",
                            Price = 200m,
                            Quantity = 200
                        });
                });

            modelBuilder.Entity("OnlineRetailShop.Domain.Models.Order", b =>
                {
                    b.HasOne("OnlineRetailShop.Domain.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}