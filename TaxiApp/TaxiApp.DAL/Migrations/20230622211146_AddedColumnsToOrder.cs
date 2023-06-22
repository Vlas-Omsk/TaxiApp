using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiApp.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleatedAt",
                table: "Order",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_TariffId",
                table: "Order",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTariffId_TariffId",
                table: "Order",
                column: "TariffId",
                principalTable: "Tariff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTariffId_TariffId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TariffId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleatedAt",
                table: "Order",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
