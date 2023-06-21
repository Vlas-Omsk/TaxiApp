using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiApp.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarTariff",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTariffCarIdTariffId", x => new { x.CarId, x.TariffId });
                    table.ForeignKey(
                        name: "FK_CarTariffCarId_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTariffTariffId_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarTariff_TariffId",
                table: "CarTariff",
                column: "TariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarTariff");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Car");
        }
    }
}
