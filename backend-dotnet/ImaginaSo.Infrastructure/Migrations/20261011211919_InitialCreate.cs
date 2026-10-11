using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImaginaSo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraCostItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraCostItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Manufacturer = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    MaterialType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    PricePerKg = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ElectricityCostPerKWh = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false),
                    MaintenanceReservePercentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Model = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Manufacturer = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    PowerConsumptionWatts = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    InvestmentReturnYears = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    HoursWorkedPerYear = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    DepreciationCostPerHour = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesChannels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    FeePercentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraCostItems_Name",
                table: "ExtraCostItems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesChannels_Name",
                table: "SalesChannels",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraCostItems");

            migrationBuilder.DropTable(
                name: "Filaments");

            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "SalesChannels");
        }
    }
}
