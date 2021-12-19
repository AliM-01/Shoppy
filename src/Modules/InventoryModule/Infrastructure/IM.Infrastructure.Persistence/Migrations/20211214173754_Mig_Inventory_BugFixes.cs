using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IM.Infrastructure.Persistence.Migrations
{
    public partial class Mig_Inventory_BugFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryOperations_Inventory_InventoryId",
                table: "InventoryOperations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InventoryOperations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryOperations_Inventory_InventoryId",
                table: "InventoryOperations",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryOperations_Inventory_InventoryId",
                table: "InventoryOperations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InventoryOperations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryOperations_Inventory_InventoryId",
                table: "InventoryOperations",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
