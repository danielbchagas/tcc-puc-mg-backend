using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Produtos.Infrastructure.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Fabricacao", "Lote", "Marca", "Nome", "Observacao", "Quantidade", "Vencimento" },
                values: new object[] { new Guid("a9007be9-0df2-47f5-a64c-55888b13cbcd"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-5454", "Pfizer", "Vacina", "Vacina contra COVID-19.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Fabricacao", "Lote", "Marca", "Nome", "Observacao", "Quantidade", "Vencimento" },
                values: new object[] { new Guid("2a55321a-1640-4628-a765-3fb113ce05d5"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-5324", "AstraZeneca", "Vacina", "Vacina contra COVID-19.", 500000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Fabricacao", "Lote", "Marca", "Nome", "Observacao", "Quantidade", "Vencimento" },
                values: new object[] { new Guid("5d67d0ad-e01e-43ba-ba50-ca8b9673f43f"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-6654", "Janssen", "Vacina", "Vacina contra COVID-19.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("2a55321a-1640-4628-a765-3fb113ce05d5"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("5d67d0ad-e01e-43ba-ba50-ca8b9673f43f"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("a9007be9-0df2-47f5-a64c-55888b13cbcd"));
        }
    }
}
