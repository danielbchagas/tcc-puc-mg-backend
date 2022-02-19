using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Ordering.Infrastructure.Migrations
{
    public partial class _100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ordering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Document = table.Column<string>(type: "varchar(18)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    FirstLine = table.Column<string>(type: "varchar(200)", nullable: false),
                    SecondLine = table.Column<string>(type: "varchar(200)", nullable: false),
                    City = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "char(2)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(9)", nullable: false),
                    Value = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<string>(type: "char(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordering", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "money", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerBasketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Ordering_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Ordering",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Ordering");
        }
    }
}
