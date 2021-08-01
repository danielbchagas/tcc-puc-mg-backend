using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("236201b0-7f2e-4d85-aff6-8637c91d0215"));

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nascimento", "Nome", "Sobrenome" },
                values: new object[] { new Guid("8d918af0-29a9-40c8-bfa0-a59e55169dfd"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "Ativo", "ClienteId", "Numero" },
                values: new object[] { new Guid("cd7540c5-5317-4c82-8412-bf1102aba0c5"), true, new Guid("8d918af0-29a9-40c8-bfa0-a59e55169dfd"), "903.142.734-92" });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Ativo", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[] { new Guid("306acc40-be0b-4c74-8255-457823015a40"), true, "Guará I", "71090-265", "Brasília", new Guid("8d918af0-29a9-40c8-bfa0-a59e55169dfd"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("cd7540c5-5317-4c82-8412-bf1102aba0c5"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("306acc40-be0b-4c74-8255-457823015a40"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("8d918af0-29a9-40c8-bfa0-a59e55169dfd"));

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nascimento", "Nome", "Sobrenome" },
                values: new object[] { new Guid("236201b0-7f2e-4d85-aff6-8637c91d0215"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" });
        }
    }
}
