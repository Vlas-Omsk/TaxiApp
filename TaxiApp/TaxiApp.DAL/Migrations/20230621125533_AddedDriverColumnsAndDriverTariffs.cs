using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiApp.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedDriverColumnsAndDriverTariffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverTariffId_TariffId",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_TariffId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "Driver");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inn",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TariffEntityId",
                table: "Driver",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverTariff",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTariffTariffIdDriverTariffDriverId", x => new { x.TariffId, x.DriverId });
                    table.ForeignKey(
                        name: "FK_DriverTariffDriverId_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverTariffTariffId_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Driver_TariffEntityId",
                table: "Driver",
                column: "TariffEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTariff_DriverId",
                table: "DriverTariff",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Tariff_TariffEntityId",
                table: "Driver",
                column: "TariffEntityId",
                principalTable: "Tariff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Tariff_TariffEntityId",
                table: "Driver");

            migrationBuilder.DropTable(
                name: "DriverTariff");

            migrationBuilder.DropIndex(
                name: "IX_Driver_TariffEntityId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "Inn",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "Passport",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "TariffEntityId",
                table: "Driver");

            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "Driver",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Driver_TariffId",
                table: "Driver",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverTariffId_TariffId",
                table: "Driver",
                column: "TariffId",
                principalTable: "Tariff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
