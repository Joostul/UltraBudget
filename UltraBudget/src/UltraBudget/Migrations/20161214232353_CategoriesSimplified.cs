using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UltraBudget.Migrations
{
    public partial class CategoriesSimplified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Currencies_CurrencyId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CurrencyId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CurrencyId",
                table: "Category",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Currencies_CurrencyId",
                table: "Category",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
