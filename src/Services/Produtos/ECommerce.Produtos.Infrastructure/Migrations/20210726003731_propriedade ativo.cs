using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Produtos.Infrastructure.Migrations
{
    public partial class propriedadeativo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("354689b2-3263-4eb1-a73a-38bb1db7ed2d"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("54ed2b84-6e7d-4f3e-a476-9ec5824c87ef"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("67efba08-4d72-46fb-abb7-000307a50f31"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("83018115-631a-42e8-81d5-5e49873feb9f"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("b90da36c-3b6d-4c1d-8180-17af88c29bf8"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("1d82fdc8-e55a-453f-baac-3f8a952c986d"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("bc3f7b48-b624-463c-84e2-f29ead62282d"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("c5d91675-4c65-42bd-a78b-9a1979be8a6d"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("e34cb927-56e5-4ee6-abd9-4c4b79a621e4"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("ee9a8291-c868-482b-9727-cc802edcc42f"));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "LogEventos",
                columns: new[] { "Id", "Momento", "OrigemRequisicao", "ProdutoId", "Uri" },
                values: new object[,]
                {
                    { new Guid("faaef97b-168a-4482-98dd-e58c88ba5ebb"), new DateTime(2021, 7, 25, 21, 37, 31, 421, DateTimeKind.Local).AddTicks(4205), "ip", "360a7834-a4b3-46de-a35f-d8c2e538e14b", "ip/produtos/novo" },
                    { new Guid("11626341-5241-44be-a91f-bf2c19cebbfd"), new DateTime(2021, 7, 25, 21, 37, 31, 422, DateTimeKind.Local).AddTicks(6741), "ip", "30d523a0-e1ef-407c-9966-e6aff159425e", "ip/produtos/novo" },
                    { new Guid("c6914bcb-9ae1-4a87-b42c-ee1fb88440cc"), new DateTime(2021, 7, 25, 21, 37, 31, 422, DateTimeKind.Local).AddTicks(6761), "ip", "7d6680ea-6651-4d17-9252-9d0dae8ad212", "ip/produtos/novo" },
                    { new Guid("f2aec7ad-f7ae-4580-a9e3-297bcdeab97d"), new DateTime(2021, 7, 25, 21, 37, 31, 422, DateTimeKind.Local).AddTicks(6764), "ip", "4f64531c-32fe-49fd-8078-96c1f6a61a2d", "ip/produtos/novo" },
                    { new Guid("c9d4eb2b-10fa-40d5-b4ed-5366a9f5a756"), new DateTime(2021, 7, 25, 21, 37, 31, 422, DateTimeKind.Local).AddTicks(6765), "ip", "cc5ac4d2-fcc9-4d4a-94d9-ecdbc6e1ed7d", "ip/produtos/novo" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "Fabricacao", "Lote", "Marca", "Nome", "Observacao", "Quantidade", "Vencimento" },
                values: new object[,]
                {
                    { new Guid("360a7834-a4b3-46de-a35f-d8c2e538e14b"), true, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-5454", "Pfizer", "Vacina", "Vacina contra COVID-19.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("30d523a0-e1ef-407c-9966-e6aff159425e"), true, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-5324", "AstraZeneca", "Vacina", "Vacina contra COVID-19.", 500000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7d6680ea-6651-4d17-9252-9d0dae8ad212"), true, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-6654", "Janssen", "Vacina", "Vacina contra COVID-19.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4f64531c-32fe-49fd-8078-96c1f6a61a2d"), true, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-1054", "GlaxoSmithKline", "Centrum", "Suplemento alimentar (multivitamínico).", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cc5ac4d2-fcc9-4d4a-94d9-ecdbc6e1ed7d"), true, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "xxx-2154", "Colgate", "Enxaguante bucal", "Enxaguante bucal.", 1000000, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("11626341-5241-44be-a91f-bf2c19cebbfd"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("c6914bcb-9ae1-4a87-b42c-ee1fb88440cc"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("c9d4eb2b-10fa-40d5-b4ed-5366a9f5a756"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("f2aec7ad-f7ae-4580-a9e3-297bcdeab97d"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("faaef97b-168a-4482-98dd-e58c88ba5ebb"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("30d523a0-e1ef-407c-9966-e6aff159425e"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("360a7834-a4b3-46de-a35f-d8c2e538e14b"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("4f64531c-32fe-49fd-8078-96c1f6a61a2d"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("7d6680ea-6651-4d17-9252-9d0dae8ad212"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("cc5ac4d2-fcc9-4d4a-94d9-ecdbc6e1ed7d"));

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Produtos");

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
    }
}
