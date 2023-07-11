using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.netCoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class forienkey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 8, 19, 28, 423, DateTimeKind.Local).AddTicks(5194));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 8, 19, 28, 423, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 8, 19, 28, 423, DateTimeKind.Local).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 8, 19, 28, 423, DateTimeKind.Local).AddTicks(5208));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8650));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8661));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 11, 4, 49, 51, 747, DateTimeKind.Local).AddTicks(8663));
        }
    }
}
