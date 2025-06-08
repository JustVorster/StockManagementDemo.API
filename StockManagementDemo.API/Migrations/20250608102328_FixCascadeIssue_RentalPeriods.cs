using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementDemo.API.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeIssue_RentalPeriods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_StockItems_StockItemId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_StockItems_StockItemId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.RenameColumn(
                name: "StockItemId",
                table: "Images",
                newName: "GarmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_StockItemId",
                table: "Images",
                newName: "IX_Images_GarmentId");

            migrationBuilder.RenameColumn(
                name: "StockItemId",
                table: "Accessories",
                newName: "GarmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Accessories_StockItemId",
                table: "Accessories",
                newName: "IX_Accessories_GarmentId");

            migrationBuilder.CreateTable(
                name: "Garments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ResalePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsResaleAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occasion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garments_Users_LenderId",
                        column: x => x.LenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    GarmentId = table.Column<int>(type: "int", nullable: false),
                    RenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalPeriods_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalPeriods_Users_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garments_LenderId",
                table: "Garments",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalPeriods_GarmentId",
                table: "RentalPeriods",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalPeriods_RenterId",
                table: "RentalPeriods",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_Garments_GarmentId",
                table: "Accessories",
                column: "GarmentId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Garments_GarmentId",
                table: "Images",
                column: "GarmentId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_Garments_GarmentId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Garments_GarmentId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "RentalPeriods");

            migrationBuilder.DropTable(
                name: "Garments");

            migrationBuilder.RenameColumn(
                name: "GarmentId",
                table: "Images",
                newName: "StockItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_GarmentId",
                table: "Images",
                newName: "IX_Images_StockItemId");

            migrationBuilder.RenameColumn(
                name: "GarmentId",
                table: "Accessories",
                newName: "StockItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Accessories_GarmentId",
                table: "Accessories",
                newName: "IX_Accessories_StockItemId");

            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DTCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DTUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KMS = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetailPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_StockItems_StockItemId",
                table: "Accessories",
                column: "StockItemId",
                principalTable: "StockItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_StockItems_StockItemId",
                table: "Images",
                column: "StockItemId",
                principalTable: "StockItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
