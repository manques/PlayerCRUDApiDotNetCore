﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asp.netCoreWebApi.Data;

#nullable disable

namespace asp.netCoreWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230710231951_forien key")]
    partial class forienkey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("asp.netCoreWebApi.models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8650),
                            Name = "anup"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8661),
                            Name = "vivek"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8662),
                            Name = "abhishek"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8663),
                            Name = "sushil"
                        });
                });

            modelBuilder.Entity("asp.netCoreWebApi.models.PlayerNumber", b =>
                {
                    b.Property<int>("PlayerNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PlayerNo");

                    b.HasIndex("PlayerID");

                    b.ToTable("PlayerNumbers");
                });

            modelBuilder.Entity("asp.netCoreWebApi.models.PlayerNumber", b =>
                {
                    b.HasOne("asp.netCoreWebApi.models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });
#pragma warning restore 612, 618
        }
    }
}