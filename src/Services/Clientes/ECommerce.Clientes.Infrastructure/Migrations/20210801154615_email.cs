using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("1fb4c6c8-0f8a-4dd5-bed4-5e38791285ef"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("7867833a-95d4-4501-9bf5-6ecb8362edb8"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("e977a1e2-a7cd-4daa-b37b-2ace22393e13"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("0e2087ac-297b-48d4-95d6-9cbafba852aa"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("b650cdb7-2b58-4d8b-8fce-8eebb45f30ca"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("f4883cd0-8f7f-4023-ae8d-8799348067af"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("7a8c2a71-916d-4594-9677-a998d31b6915"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("7b8c82a3-3071-4245-a42d-2641ef7e12fe"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("7e3d69f7-0286-4cad-8c0c-f4801bd8f459"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("0df62fab-1b4a-4558-b460-7f42cee0702c"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("643cff0f-92f9-4b1f-8871-7efed529dd37"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("7c1469cf-f765-40f2-adb2-c45511e27b4e"));

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nascimento", "Nome", "Sobrenome" },
                values: new object[,]
                {
                    { new Guid("ba07b2f0-8928-481f-b604-c0bd8b27370a"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" },
                    { new Guid("e80b1055-5d13-4bb4-a940-f449dd099f22"), true, new DateTime(1963, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayla Caroline", "Ana Gomes" },
                    { new Guid("dbd6065d-e021-43ea-be6f-cf741b530edd"), true, new DateTime(1975, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "BetinaFlávia", "Souza" }
                });

            migrationBuilder.InsertData(
                table: "LogEventos",
                columns: new[] { "Id", "ClienteId", "Momento", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("652ddfe9-cbdf-4aa6-8c5e-58af89044bff"), "ba07b2f0-8928-481f-b604-c0bd8b27370a", new DateTime(2021, 8, 1, 12, 46, 14, 810, DateTimeKind.Local).AddTicks(5573), "08303b1f-fa0d-402c-b1a5-88210780107a" },
                    { new Guid("f622ac48-72a0-4a85-a5f0-d6561a4a16d6"), "e80b1055-5d13-4bb4-a940-f449dd099f22", new DateTime(2021, 8, 1, 12, 46, 14, 811, DateTimeKind.Local).AddTicks(6390), "08303b1f-fa0d-402c-b1a5-88210780107a" },
                    { new Guid("4fa86868-a906-4813-ad8a-1406824cffdf"), "dbd6065d-e021-43ea-be6f-cf741b530edd", new DateTime(2021, 8, 1, 12, 46, 14, 811, DateTimeKind.Local).AddTicks(6406), "08303b1f-fa0d-402c-b1a5-88210780107a" }
                });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("9f5e38a9-8717-42a4-843a-c62b6b776b10"), new Guid("ba07b2f0-8928-481f-b604-c0bd8b27370a"), "903.142.734-92" },
                    { new Guid("87dac7c0-f1fe-48e1-a7d0-e15dee5a332d"), new Guid("e80b1055-5d13-4bb4-a940-f449dd099f22"), "668.154.787-77" },
                    { new Guid("0f78ab8a-23d4-41fc-9e25-ff0ceba221d3"), new Guid("dbd6065d-e021-43ea-be6f-cf741b530edd"), "345.712.047-10" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[,]
                {
                    { new Guid("904f2679-4e43-442e-a65b-cc6ecadda001"), new Guid("ba07b2f0-8928-481f-b604-c0bd8b27370a"), "davi_giovanni_felipe@gmail.com" },
                    { new Guid("0fb67b13-9125-487e-a5d3-aa5fcc1dfe7e"), new Guid("e80b1055-5d13-4bb4-a940-f449dd099f22"), "ayla_caroline_ana_gomes@gmail.com" },
                    { new Guid("da638558-3ccb-48e3-960e-5175ab93047f"), new Guid("dbd6065d-e021-43ea-be6f-cf741b530edd"), "b_etina_flavia_souza@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[,]
                {
                    { new Guid("354f1a68-3765-4e47-8c2c-56311e356009"), "Guará I", "71090-265", "Brasília", new Guid("ba07b2f0-8928-481f-b604-c0bd8b27370a"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" },
                    { new Guid("013c755f-414f-4281-804a-4bc1227499d8"), "Tarumã", "82530-220", "Curitiba", new Guid("e80b1055-5d13-4bb4-a940-f449dd099f22"), "PR", "Praça São Francisco de Assis, 442" },
                    { new Guid("cbdc9ac1-9b01-4331-88ef-4b9eece177d2"), "Abegay", "98045-115", "Cruz Alta", new Guid("dbd6065d-e021-43ea-be6f-cf741b530edd"), "RS", "Rua Neves, 378" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("0f78ab8a-23d4-41fc-9e25-ff0ceba221d3"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("87dac7c0-f1fe-48e1-a7d0-e15dee5a332d"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("9f5e38a9-8717-42a4-843a-c62b6b776b10"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("0fb67b13-9125-487e-a5d3-aa5fcc1dfe7e"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("904f2679-4e43-442e-a65b-cc6ecadda001"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("da638558-3ccb-48e3-960e-5175ab93047f"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("013c755f-414f-4281-804a-4bc1227499d8"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("354f1a68-3765-4e47-8c2c-56311e356009"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("cbdc9ac1-9b01-4331-88ef-4b9eece177d2"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("4fa86868-a906-4813-ad8a-1406824cffdf"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("652ddfe9-cbdf-4aa6-8c5e-58af89044bff"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("f622ac48-72a0-4a85-a5f0-d6561a4a16d6"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("ba07b2f0-8928-481f-b604-c0bd8b27370a"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("dbd6065d-e021-43ea-be6f-cf741b530edd"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("e80b1055-5d13-4bb4-a940-f449dd099f22"));

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nascimento", "Nome", "Sobrenome" },
                values: new object[,]
                {
                    { new Guid("7c1469cf-f765-40f2-adb2-c45511e27b4e"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" },
                    { new Guid("0df62fab-1b4a-4558-b460-7f42cee0702c"), true, new DateTime(1963, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayla Caroline", "Ana Gomes" },
                    { new Guid("643cff0f-92f9-4b1f-8871-7efed529dd37"), true, new DateTime(1975, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "BetinaFlávia", "Souza" }
                });

            migrationBuilder.InsertData(
                table: "LogEventos",
                columns: new[] { "Id", "ClienteId", "Momento", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("7e3d69f7-0286-4cad-8c0c-f4801bd8f459"), "7c1469cf-f765-40f2-adb2-c45511e27b4e", new DateTime(2021, 8, 1, 12, 40, 10, 902, DateTimeKind.Local).AddTicks(4722), "003a608e-30c5-4deb-b480-65205cfab9cd" },
                    { new Guid("7a8c2a71-916d-4594-9677-a998d31b6915"), "0df62fab-1b4a-4558-b460-7f42cee0702c", new DateTime(2021, 8, 1, 12, 40, 10, 909, DateTimeKind.Local).AddTicks(3850), "003a608e-30c5-4deb-b480-65205cfab9cd" },
                    { new Guid("7b8c82a3-3071-4245-a42d-2641ef7e12fe"), "643cff0f-92f9-4b1f-8871-7efed529dd37", new DateTime(2021, 8, 1, 12, 40, 10, 909, DateTimeKind.Local).AddTicks(3866), "003a608e-30c5-4deb-b480-65205cfab9cd" }
                });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("1fb4c6c8-0f8a-4dd5-bed4-5e38791285ef"), new Guid("7c1469cf-f765-40f2-adb2-c45511e27b4e"), "903.142.734-92" },
                    { new Guid("e977a1e2-a7cd-4daa-b37b-2ace22393e13"), new Guid("0df62fab-1b4a-4558-b460-7f42cee0702c"), "668.154.787-77" },
                    { new Guid("7867833a-95d4-4501-9bf5-6ecb8362edb8"), new Guid("643cff0f-92f9-4b1f-8871-7efed529dd37"), "345.712.047-10" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[,]
                {
                    { new Guid("f4883cd0-8f7f-4023-ae8d-8799348067af"), "Guará I", "71090-265", "Brasília", new Guid("7c1469cf-f765-40f2-adb2-c45511e27b4e"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" },
                    { new Guid("0e2087ac-297b-48d4-95d6-9cbafba852aa"), "Tarumã", "82530-220", "Curitiba", new Guid("0df62fab-1b4a-4558-b460-7f42cee0702c"), "PR", "Praça São Francisco de Assis, 442" },
                    { new Guid("b650cdb7-2b58-4d8b-8fce-8eebb45f30ca"), "Abegay", "98045-115", "Cruz Alta", new Guid("643cff0f-92f9-4b1f-8871-7efed529dd37"), "RS", "Rua Neves, 378" }
                });
        }
    }
}
