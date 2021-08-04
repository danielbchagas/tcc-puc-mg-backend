using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class datanasc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Nascimento",
                table: "Clientes",
                newName: "DataNascimento");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "DataNascimento", "Nome", "Sobrenome" },
                values: new object[,]
                {
                    { new Guid("590999e5-8365-42f8-a08c-0e1f463d4d44"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" },
                    { new Guid("8c7f9a0e-f796-4a11-82f6-56db88db9112"), true, new DateTime(1963, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayla Caroline", "Ana Gomes" },
                    { new Guid("db66191d-b1e1-466f-8efa-9eaa0aa1db1b"), true, new DateTime(1975, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "BetinaFlávia", "Souza" }
                });

            migrationBuilder.InsertData(
                table: "LogEventos",
                columns: new[] { "Id", "ClienteId", "Momento", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("f60583b4-7179-4ce8-8a68-55bb72716da5"), "590999e5-8365-42f8-a08c-0e1f463d4d44", new DateTime(2021, 8, 4, 13, 24, 36, 652, DateTimeKind.Local).AddTicks(6096), "fe01b5a0-36dd-4c04-98c2-5871fd66bdb9" },
                    { new Guid("dce4a001-6407-4e44-9449-5586566d165c"), "8c7f9a0e-f796-4a11-82f6-56db88db9112", new DateTime(2021, 8, 4, 13, 24, 36, 653, DateTimeKind.Local).AddTicks(7626), "fe01b5a0-36dd-4c04-98c2-5871fd66bdb9" },
                    { new Guid("e305e03c-c3de-4837-8a10-c8632348d5c5"), "db66191d-b1e1-466f-8efa-9eaa0aa1db1b", new DateTime(2021, 8, 4, 13, 24, 36, 653, DateTimeKind.Local).AddTicks(7641), "fe01b5a0-36dd-4c04-98c2-5871fd66bdb9" }
                });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("46e53615-5e1a-4dde-b68b-cb7fa3a61620"), new Guid("590999e5-8365-42f8-a08c-0e1f463d4d44"), "903.142.734-92" },
                    { new Guid("f7fb87a4-8e2d-4f8b-861a-f35f7d02117a"), new Guid("8c7f9a0e-f796-4a11-82f6-56db88db9112"), "668.154.787-77" },
                    { new Guid("caca8c60-ee66-44bd-ab47-3e7546a260c4"), new Guid("db66191d-b1e1-466f-8efa-9eaa0aa1db1b"), "345.712.047-10" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[,]
                {
                    { new Guid("846c1d1c-179b-40dd-8a23-6edbe5e49275"), new Guid("590999e5-8365-42f8-a08c-0e1f463d4d44"), "davi_giovanni_felipe@gmail.com" },
                    { new Guid("f04cdec9-a82c-4a56-aad8-7f275538ffd1"), new Guid("8c7f9a0e-f796-4a11-82f6-56db88db9112"), "ayla_caroline_ana_gomes@gmail.com" },
                    { new Guid("7d50d3ff-04f7-4326-ada9-f52f8572e964"), new Guid("db66191d-b1e1-466f-8efa-9eaa0aa1db1b"), "b_etina_flavia_souza@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[,]
                {
                    { new Guid("75ff630b-d83a-4ccf-b9d2-0cf04ede50fb"), "Guará I", "71090-265", "Brasília", new Guid("590999e5-8365-42f8-a08c-0e1f463d4d44"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" },
                    { new Guid("817d5cfc-2819-4d0e-bc66-1ee1c19aee67"), "Tarumã", "82530-220", "Curitiba", new Guid("8c7f9a0e-f796-4a11-82f6-56db88db9112"), "PR", "Praça São Francisco de Assis, 442" },
                    { new Guid("8c32f287-8d36-49c5-ba31-eb6873d97943"), "Abegay", "98045-115", "Cruz Alta", new Guid("db66191d-b1e1-466f-8efa-9eaa0aa1db1b"), "RS", "Rua Neves, 378" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("46e53615-5e1a-4dde-b68b-cb7fa3a61620"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("caca8c60-ee66-44bd-ab47-3e7546a260c4"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("f7fb87a4-8e2d-4f8b-861a-f35f7d02117a"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("7d50d3ff-04f7-4326-ada9-f52f8572e964"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("846c1d1c-179b-40dd-8a23-6edbe5e49275"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("f04cdec9-a82c-4a56-aad8-7f275538ffd1"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("75ff630b-d83a-4ccf-b9d2-0cf04ede50fb"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("817d5cfc-2819-4d0e-bc66-1ee1c19aee67"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("8c32f287-8d36-49c5-ba31-eb6873d97943"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("dce4a001-6407-4e44-9449-5586566d165c"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("e305e03c-c3de-4837-8a10-c8632348d5c5"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("f60583b4-7179-4ce8-8a68-55bb72716da5"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("590999e5-8365-42f8-a08c-0e1f463d4d44"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("8c7f9a0e-f796-4a11-82f6-56db88db9112"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("db66191d-b1e1-466f-8efa-9eaa0aa1db1b"));

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "Clientes",
                newName: "Nascimento");

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
    }
}
