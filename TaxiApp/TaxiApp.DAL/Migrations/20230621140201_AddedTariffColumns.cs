using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiApp.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedTariffColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "FreeWaiting",
                table: "Tariff",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<decimal>(
                name: "InCityPricePerKm",
                table: "Tariff",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutsideCityPricePerKm",
                table: "Tariff",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidWaitingPricePerMin",
                table: "Tariff",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WaitingOnWayPricePerMin",
                table: "Tariff",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AdditionalService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TariffAdditionalService",
                columns: table => new
                {
                    TariffId = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffAdditionalServiceTariffIdAdditionalServiceId", x => new { x.TariffId, x.AdditionalServiceId });
                    table.ForeignKey(
                        name: "FK_TariffAdditionalServiceAdditionalServiceId_AdditionalServiceId",
                        column: x => x.AdditionalServiceId,
                        principalTable: "AdditionalService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffAdditionalServiceTariffId_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TariffAdditionalService_AdditionalServiceId",
                table: "TariffAdditionalService",
                column: "AdditionalServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TariffAdditionalService");

            migrationBuilder.DropTable(
                name: "AdditionalService");

            migrationBuilder.DropColumn(
                name: "FreeWaiting",
                table: "Tariff");

            migrationBuilder.DropColumn(
                name: "InCityPricePerKm",
                table: "Tariff");

            migrationBuilder.DropColumn(
                name: "OutsideCityPricePerKm",
                table: "Tariff");

            migrationBuilder.DropColumn(
                name: "PaidWaitingPricePerMin",
                table: "Tariff");

            migrationBuilder.DropColumn(
                name: "WaitingOnWayPricePerMin",
                table: "Tariff");
        }
    }
}
