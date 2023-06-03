using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    public partial class tablesrenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItems",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.RenameTable(
                name: "BasketItems",
                schema: "Basket",
                newName: "Item",
                newSchema: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "BasketItems",
                newSchema: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_Item_BasketId",
                schema: "Basket",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                schema: "Basket",
                table: "BasketItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                schema: "Basket",
                table: "BasketItems",
                column: "BasketId",
                principalSchema: "Basket",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
