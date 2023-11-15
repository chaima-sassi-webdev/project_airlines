﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_airlines.Data;

#nullable disable

namespace project_airlines.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231003144441_MarketDetails")]
    partial class MarketDetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("project_airlines.Models.GlobalTraffic.GlobalTraffic", b =>
                {
                    b.Property<int>("YEAR")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("YEAR"), 1L, 1);

                    b.Property<int>("BOOKED")
                        .HasColumnType("int");

                    b.Property<double>("CR")
                        .HasColumnType("float");

                    b.Property<int>("FLOWN")
                        .HasColumnType("int");

                    b.HasKey("YEAR");

                    b.ToTable("GlobalTraffic");
                });

            modelBuilder.Entity("project_airlines.Models.Market_Details.MarketDetails", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AC_REGISTRATION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ARR_AP_ACTUAL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AVG_FARE")
                        .HasColumnType("int");

                    b.Property<string>("DAY_OF_ORIGIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ESCALESTAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FN_CARRIER")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FN_NUMBER")
                        .HasColumnType("int");

                    b.Property<int>("PAX_BOOKED_C")
                        .HasColumnType("int");

                    b.Property<int>("PAX_BOOKED_Y")
                        .HasColumnType("int");

                    b.Property<int>("PAX_FLOWN_C")
                        .HasColumnType("int");

                    b.Property<int>("PAX_FLOWN_Y")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Market_Details");
                });

            modelBuilder.Entity("project_airlines.Models.Market.Market", b =>
                {
                    b.Property<string>("escale_D")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("pays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paysArRapport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paysArabe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zonegeo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zonegeoarabe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("escale_D");

                    b.ToTable("Market");
                });

            modelBuilder.Entity("project_airlines.Models.Supplier.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("SupplierImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("mobile_phone")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });
#pragma warning restore 612, 618
        }
    }
}
