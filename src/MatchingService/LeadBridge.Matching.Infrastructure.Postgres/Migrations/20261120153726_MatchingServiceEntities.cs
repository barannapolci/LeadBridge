using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadBridge.Matching.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class MatchingServiceEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndexedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestVersion = table.Column<int>(type: "integer", nullable: false),
                    Side = table.Column<int>(type: "integer", nullable: false),
                    CategoryKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Conditions = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexedItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndexedItems_Id",
                table: "IndexedItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IndexedItems_RequestId",
                table: "IndexedItems",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexedItems_Side_CategoryKey",
                table: "IndexedItems",
                columns: new[] { "Side", "CategoryKey" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexedItems");
        }
    }
}
