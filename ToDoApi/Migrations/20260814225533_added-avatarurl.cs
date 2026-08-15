using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class addedavatarurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvatarUrl", "Created_At", "Password" },
                values: new object[] { null, new DateTime(2026, 8, 14, 19, 55, 32, 358, DateTimeKind.Local).AddTicks(4127), "$2a$13$LSoMeUkh1pKqYXtAfyyJ6.qEpsp4MIDGQQ9bn3y/kOx472XHeD98a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created_At", "Password" },
                values: new object[] { new DateTime(2026, 7, 28, 18, 24, 0, 921, DateTimeKind.Local).AddTicks(4940), "$2a$13$2ZzswhFj6gs0lzwE35JuLe8r7qtoJAYYQIF7at.hq41ZnsLOW1.4y" });
        }
    }
}
