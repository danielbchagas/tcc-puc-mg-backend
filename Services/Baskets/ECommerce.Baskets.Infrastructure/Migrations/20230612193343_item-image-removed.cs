using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    public partial class itemimageremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "Basket",
                table: "Items");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Basket",
                table: "Items",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Basket",
                table: "Items",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Basket",
                table: "Items",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Basket",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Basket",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Basket",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "Basket",
                table: "Items",
                type: "text",
                nullable: true);
        }
    }
}
