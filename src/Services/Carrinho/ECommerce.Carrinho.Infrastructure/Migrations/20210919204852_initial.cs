using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Carrinho.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrinho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCarrinho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "varchar(20)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    Imagem = table.Column<string>(type: "text", nullable: true),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CarrinhoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCarrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCarrinho_Carrinho_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrinho_ClienteId",
                table: "Carrinho",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarrinho_CarrinhoId",
                table: "ItemCarrinho",
                column: "CarrinhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCarrinho");

            migrationBuilder.DropTable(
                name: "Carrinho");
        }
    }
}
