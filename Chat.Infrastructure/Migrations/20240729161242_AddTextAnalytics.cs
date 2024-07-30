using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTextAnalytics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d181f3f5-b3ea-4a20-acaa-e44f84c32fe4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fde31506-051b-46c3-a3c3-f8c54ec91bda"));

            migrationBuilder.AddColumn<int>(
                name: "Sentiment",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2d5ec523-3ac3-47b0-bb85-f9044c19d6f8"), null, "User", "USER" },
                    { new Guid("ddffd4e8-03a3-4501-acb8-df173eecd4e9"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2d5ec523-3ac3-47b0-bb85-f9044c19d6f8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ddffd4e8-03a3-4501-acb8-df173eecd4e9"));

            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "Message");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("d181f3f5-b3ea-4a20-acaa-e44f84c32fe4"), null, "Admin", "ADMIN" },
                    { new Guid("fde31506-051b-46c3-a3c3-f8c54ec91bda"), null, "User", "USER" }
                });
        }
    }
}
