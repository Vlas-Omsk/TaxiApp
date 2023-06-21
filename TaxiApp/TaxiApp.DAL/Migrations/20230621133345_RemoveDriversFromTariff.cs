using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiApp.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDriversFromTariff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Tariff_TariffEntityId",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_TariffEntityId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "TariffEntityId",
                table: "Driver");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TariffEntityId",
                table: "Driver",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Driver_TariffEntityId",
                table: "Driver",
                column: "TariffEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Tariff_TariffEntityId",
                table: "Driver",
                column: "TariffEntityId",
                principalTable: "Tariff",
                principalColumn: "Id");
        }
    }
}
