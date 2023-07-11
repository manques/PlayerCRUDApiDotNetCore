using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp.netCoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class forienkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "PlayerNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_PlayerNumbers_PlayerID",
                table: "PlayerNumbers",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerNumbers_Players_PlayerID",
                table: "PlayerNumbers",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerNumbers_Players_PlayerID",
                table: "PlayerNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PlayerNumbers_PlayerID",
                table: "PlayerNumbers");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "PlayerNumbers");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 10, 16, 20, 1, 241, DateTimeKind.Local).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 10, 16, 20, 1, 241, DateTimeKind.Local).AddTicks(9409));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 10, 16, 20, 1, 241, DateTimeKind.Local).AddTicks(9410));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 7, 10, 16, 20, 1, 241, DateTimeKind.Local).AddTicks(9411));
        }
    }
}
