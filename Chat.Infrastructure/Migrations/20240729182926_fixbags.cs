using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixbags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2d5ec523-3ac3-47b0-bb85-f9044c19d6f8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ddffd4e8-03a3-4501-acb8-df173eecd4e9"));

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Message",
                newName: "Date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("760904cb-311e-48a4-9054-a701cf3f7aac"), null, "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("760904cb-311e-48a4-9054-a701cf3f7aac"));

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Message",
                newName: "Time");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2d5ec523-3ac3-47b0-bb85-f9044c19d6f8"), null, "User", "USER" },
                    { new Guid("ddffd4e8-03a3-4501-acb8-df173eecd4e9"), null, "Admin", "ADMIN" }
                });
        }
    }
}
