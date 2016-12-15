using Microsoft.EntityFrameworkCore.Migrations;

namespace UltraBudget.Migrations
{
    public partial class Greetings2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AllGreetings",
                table: "AllGreetings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Greetings",
                table: "AllGreetings",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "AllGreetings",
                newName: "Greetings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Greetings",
                table: "Greetings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllGreetings",
                table: "Greetings",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "Greetings",
                newName: "AllGreetings");
        }
    }
}
