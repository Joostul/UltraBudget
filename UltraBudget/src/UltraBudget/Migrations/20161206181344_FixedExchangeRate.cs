using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UltraBudget.Migrations
{
    public partial class FixedExchangeRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_ExchangeRate_ExchangeRateId",
                table: "Currency");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Currency_CurrencyId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Currency_ExchangeRateId",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "ExchangeRateId",
                table: "Currency");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "ExchangeRate",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRate_Currencies_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Currencies_CurrencyId",
                table: "Wallets",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRate_Currencies_CurrencyId",
                table: "ExchangeRate");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Currencies_CurrencyId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRate_CurrencyId",
                table: "ExchangeRate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ExchangeRate");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeRateId",
                table: "Currencies",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currencies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_ExchangeRateId",
                table: "Currencies",
                column: "ExchangeRateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_ExchangeRate_ExchangeRateId",
                table: "Currencies",
                column: "ExchangeRateId",
                principalTable: "ExchangeRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Currency_CurrencyId",
                table: "Wallets",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");
        }
    }
}
