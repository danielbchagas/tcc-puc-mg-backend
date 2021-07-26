using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Produtos.Infrastructure.Migrations
{
    public partial class primeiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrigemRequisicao = table.Column<string>(type: "varchar(50)", nullable: false),
                    Momento = table.Column<DateTime>(type: "date", nullable: false),
                    Uri = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProdutoId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEventos", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "LogEventos",
                columns: new[] { "Id", "Momento", "OrigemRequisicao", "ProdutoId", "Uri" },
                values: new object[,]
                {
                    { new Guid("354689b2-3263-4eb1-a73a-38bb1db7ed2d"), new DateTime(2021, 7, 25, 20, 57, 45, 81, DateTimeKind.Local).AddTicks(2379), "ip", "bc3f7b48-b624-463c-84e2-f29ead62282d", "ip/produtos/novo" },
                    { new Guid("b90da36c-3b6d-4c1d-8180-17af88c29bf8"), new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4587), "ip", "e34cb927-56e5-4ee6-abd9-4c4b79a621e4", "ip/produtos/novo" },
                    { new Guid("83018115-631a-42e8-81d5-5e49873feb9f"), new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4607), "ip", "ee9a8291-c868-482b-9727-cc802edcc42f", "ip/produtos/novo" },
                    { new Guid("67efba08-4d72-46fb-abb7-000307a50f31"), new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4617), "ip", "1d82fdc8-e55a-453f-baac-3f8a952c986d", "ip/produtos/novo" },
                    { new Guid("54ed2b84-6e7d-4f3e-a476-9ec5824c87ef"), new DateTime(2021, 7, 25, 20, 57, 45, 82, DateTimeKind.Local).AddTicks(4619), "ip", "c5d91675-4c65-42bd-a78b-9a1979be8a6d", "ip/produtos/novo" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Fabricacao", "Lote", "Marca", "Nome", "Observacao", "Quantidade", "Vencimento" },
                values: new object[,]
                {
                    { new Guid("bc3f7b48-b624-463c-84e2-f29ead62282d"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-5454", "Pfizer", "Vacina", "Vacina contra COVID-19.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e34cb927-56e5-4ee6-abd9-4c4b79a621e4"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-5324", "AstraZeneca", "Vacina", "Vacina contra COVID-19.", 500000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ee9a8291-c868-482b-9727-cc802edcc42f"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-6654", "Janssen", "Vacina", "Vacina contra COVID-19.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1d82fdc8-e55a-453f-baac-3f8a952c986d"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-1054", "GlaxoSmithKline", "Centrum", "Suplemento alimentar (multivitamínico).", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c5d91675-4c65-42bd-a78b-9a1979be8a6d"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-2154", "Colgate", "Enxaguante bucal", "Enxaguante bucal.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEventos");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
