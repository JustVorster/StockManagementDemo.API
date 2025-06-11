using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementDemo.API.Migrations
{
    /// <inheritdoc />
    public partial class NewChange01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentAnalyticsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentAnalyticsLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentAnalyticsLogs_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentAnalyticsLogs_GarmentId",
                table: "GarmentAnalyticsLogs",
                column: "GarmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentAnalyticsLogs");
        }
    }
}
