using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Produtos.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Marca = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fabricacao = table.Column<DateTime>(type: "date", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "date", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
