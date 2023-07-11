using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.netCoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class add1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 20, 44, 29, 235, DateTimeKind.Local).AddTicks(3404));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 20, 44, 29, 235, DateTimeKind.Local).AddTicks(3418));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 20, 44, 29, 235, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 20, 44, 29, 235, DateTimeKind.Local).AddTicks(3421));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6693));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6694));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 9, 11, 30, 8, 854, DateTimeKind.Local).AddTicks(6695));
        }
    }
}
