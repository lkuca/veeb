﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using veeb.Data;

#nullable disable

namespace veeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241204213204_serkiktr")]
    partial class serkiktr
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("veeb.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("KasutajaId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("veeb.Models.Kasutaja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Eesnimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kasutajanimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parool")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Perenimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique()
                        .HasFilter("[CartId] IS NOT NULL");

                    b.ToTable("Kasutajad");
                });

            modelBuilder.Entity("veeb.Models.Toode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Quantity")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Tooted");
                });

            modelBuilder.Entity("veeb.Models.Kasutaja", b =>
                {
                    b.HasOne("veeb.Models.Cart", "Cart")
                        .WithOne("Kasutaja")
                        .HasForeignKey("veeb.Models.Kasutaja", "CartId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("veeb.Models.Toode", b =>
                {
                    b.HasOne("veeb.Models.Cart", "Cart")
                        .WithMany("Tooted")
                        .HasForeignKey("CartId");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("veeb.Models.Cart", b =>
                {
                    b.Navigation("Kasutaja");

                    b.Navigation("Tooted");
                });
#pragma warning restore 612, 618
        }
    }
}
