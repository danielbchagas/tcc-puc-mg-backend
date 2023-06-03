using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    public partial class newstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_ShoppingBasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_ShoppingBasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "ShoppingBasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                schema: "Basket",
                table: "BasketItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                schema: "Basket",
                table: "BasketItems",
                column: "BasketId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "BasketId",
                schema: "Basket",
                table: "BasketItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingBasketId",
                schema: "Basket",
                table: "BasketItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ShoppingBasketId",
                schema: "Basket",
                table: "BasketItems",
                column: "ShoppingBasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_ShoppingBasketId",
                schema: "Basket",
                table: "BasketItems",
                column: "ShoppingBasketId",
                principalSchema: "Basket",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
