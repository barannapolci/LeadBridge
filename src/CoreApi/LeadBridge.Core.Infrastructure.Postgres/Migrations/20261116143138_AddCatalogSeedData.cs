using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadBridge.Core.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddCatalogSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Key", "Name" },
                values: new object[,]
                {
                    { new Guid("80000000-0000-0000-0000-000000000001"), "antifreeze", "Антифриз" },
                    { new Guid("80000000-0000-0000-0000-000000000002"), "combine-oil", "Масло для комбайна" }
                });

            migrationBuilder.InsertData(
                table: "AttributeDefinitions",
                columns: new[] { "Id", "AllowedValues", "CategoryId", "Key", "Name", "Type", "Unit" },
                values: new object[,]
                {
                    { new Guid("90000000-0000-0000-0000-000000000001"), new[] { "A", "B", "C" }, new Guid("80000000-0000-0000-0000-000000000001"), "manufacturer", "Виробник", 1, null },
                    { new Guid("90000000-0000-0000-0000-000000000002"), new string[0], new Guid("80000000-0000-0000-0000-000000000001"), "price", "Ціна", 0, "грн" },
                    { new Guid("90000000-0000-0000-0000-000000000003"), new string[0], new Guid("80000000-0000-0000-0000-000000000001"), "volume", "Об'єм", 0, "л" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "AttributeDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "AttributeDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000001"));
        }
    }
}
