using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    public partial class productidremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Baskets_BasketId",
                schema: "Basket",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Basket",
                table: "Items");

            migrationBuilder.AlterColumn<Guid>(
                name: "BasketId",
                schema: "Basket",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Baskets_BasketId",
                schema: "Basket",
                table: "Items",
                column: "BasketId",
                principalSchema: "Basket",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Baskets_BasketId",
                schema: "Basket",
                table: "Items");

            migrationBuilder.AlterColumn<Guid>(
                name: "BasketId",
                schema: "Basket",
                table: "Items",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "Basket",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
