using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    public partial class tablesitemrenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Baskets_BasketId",
                schema: "Basket",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                schema: "Basket",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                schema: "Basket",
                newName: "Items",
                newSchema: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_Item_BasketId",
                schema: "Basket",
                table: "Items",
                newName: "IX_Items_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                schema: "Basket",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Baskets_BasketId",
                schema: "Basket",
                table: "Items",
                column: "BasketId",
                principalSchema: "Basket",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Baskets_BasketId",
                schema: "Basket",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                schema: "Basket",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                schema: "Basket",
                newName: "Item",
                newSchema: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_Items_BasketId",
                schema: "Basket",
                table: "Item",
                newName: "IX_Item_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                schema: "Basket",
                table: "Item",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Baskets_BasketId",
                schema: "Basket",
                table: "Item",
                column: "BasketId",
                principalSchema: "Basket",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
