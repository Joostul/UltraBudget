using Microsoft.EntityFrameworkCore.Migrations;

namespace UltraBudget.Migrations
{
    public partial class Wallets2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_WalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Currency_CurrencyId",
                table: "Wallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Currency_CurrencyId",
                table: "Wallet",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Wallet_CurrencyId",
                table: "Wallet",
                newName: "IX_Wallets_CurrencyId");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "Wallets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Currency_CurrencyId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Currency_CurrencyId",
                table: "Wallets",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_CurrencyId",
                table: "Wallets",
                newName: "IX_Wallet_CurrencyId");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallet");
        }
    }
}
