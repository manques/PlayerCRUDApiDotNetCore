using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace asp.netCoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addPlayer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6681), "anup" },
                    { 2, new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6693), "vivek" },
                    { 3, new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6694), "abhishek" },
                    { 4, new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6695), "sushil" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
