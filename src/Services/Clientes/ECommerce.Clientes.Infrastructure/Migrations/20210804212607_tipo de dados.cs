using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class tipodedados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "LogEventos",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "LogEventos",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Enderecos",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Emails",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Documentos",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "DataNascimento", "Nome", "Sobrenome" },
                values: new object[,]
                {
                    { new Guid("6d47bc81-81fc-4545-9c64-3c323aa9d260"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" },
                    { new Guid("546aeb4e-a7c3-4c53-a661-c2671b2b7a50"), true, new DateTime(1963, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayla Caroline", "Ana Gomes" },
                    { new Guid("715f967a-9ff9-4490-a6b2-567efa55c317"), true, new DateTime(1975, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "BetinaFlávia", "Souza" }
                });

            migrationBuilder.InsertData(
                table: "LogEventos",
                columns: new[] { "Id", "ClienteId", "Momento", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("078575ef-dab0-4c1d-827e-2ab4b7bc0651"), new Guid("6d47bc81-81fc-4545-9c64-3c323aa9d260"), new DateTime(2021, 8, 4, 18, 26, 7, 24, DateTimeKind.Local).AddTicks(4309), new Guid("80e75f75-8340-44af-b97c-b24229b9ebb0") },
                    { new Guid("2c3336e7-5098-4002-863d-de5135aa36f9"), new Guid("546aeb4e-a7c3-4c53-a661-c2671b2b7a50"), new DateTime(2021, 8, 4, 18, 26, 7, 26, DateTimeKind.Local).AddTicks(955), new Guid("80e75f75-8340-44af-b97c-b24229b9ebb0") },
                    { new Guid("12a60a99-3d91-4337-b740-6a24a8849953"), new Guid("715f967a-9ff9-4490-a6b2-567efa55c317"), new DateTime(2021, 8, 4, 18, 26, 7, 26, DateTimeKind.Local).AddTicks(978), new Guid("80e75f75-8340-44af-b97c-b24229b9ebb0") }
                });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("48d454ad-b6b8-490a-a350-948898ac7ca1"), new Guid("6d47bc81-81fc-4545-9c64-3c323aa9d260"), "903.142.734-92" },
                    { new Guid("68fa8bfc-2871-4183-bdad-ab3310f71df6"), new Guid("546aeb4e-a7c3-4c53-a661-c2671b2b7a50"), "668.154.787-77" },
                    { new Guid("54b55a45-2761-4201-9709-52bf836b8306"), new Guid("715f967a-9ff9-4490-a6b2-567efa55c317"), "345.712.047-10" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[,]
                {
                    { new Guid("f8fd7c54-ce68-4dd6-97e2-afa0de5794ec"), new Guid("6d47bc81-81fc-4545-9c64-3c323aa9d260"), "davi_giovanni_felipe@gmail.com" },
                    { new Guid("f3e6151a-3c5d-41ea-9995-f684373dcdd2"), new Guid("546aeb4e-a7c3-4c53-a661-c2671b2b7a50"), "ayla_caroline_ana_gomes@gmail.com" },
                    { new Guid("04dde317-6f91-4366-925d-7ca950db10db"), new Guid("715f967a-9ff9-4490-a6b2-567efa55c317"), "b_etina_flavia_souza@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[,]
                {
                    { new Guid("c978a3b2-d4a6-4764-8ad4-df5076cbfd76"), "Guará I", "71090-265", "Brasília", new Guid("6d47bc81-81fc-4545-9c64-3c323aa9d260"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" },
                    { new Guid("a5c1af46-0de9-43de-a4ec-a0e7305bc8af"), "Tarumã", "82530-220", "Curitiba", new Guid("546aeb4e-a7c3-4c53-a661-c2671b2b7a50"), "PR", "Praça São Francisco de Assis, 442" },
                    { new Guid("fe0390f2-dba6-46c8-9dc0-53d06bd1042c"), "Abegay", "98045-115", "Cruz Alta", new Guid("715f967a-9ff9-4490-a6b2-567efa55c317"), "RS", "Rua Neves, 378" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("48d454ad-b6b8-490a-a350-948898ac7ca1"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("54b55a45-2761-4201-9709-52bf836b8306"));

            migrationBuilder.DeleteData(
                table: "Documentos",
                keyColumn: "Id",
                keyValue: new Guid("68fa8bfc-2871-4183-bdad-ab3310f71df6"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("04dde317-6f91-4366-925d-7ca950db10db"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("f3e6151a-3c5d-41ea-9995-f684373dcdd2"));

            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: new Guid("f8fd7c54-ce68-4dd6-97e2-afa0de5794ec"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("a5c1af46-0de9-43de-a4ec-a0e7305bc8af"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("c978a3b2-d4a6-4764-8ad4-df5076cbfd76"));

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "Id",
                keyValue: new Guid("fe0390f2-dba6-46c8-9dc0-53d06bd1042c"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("078575ef-dab0-4c1d-827e-2ab4b7bc0651"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("12a60a99-3d91-4337-b740-6a24a8849953"));

            migrationBuilder.DeleteData(
                table: "LogEventos",
                keyColumn: "Id",
                keyValue: new Guid("2c3336e7-5098-4002-863d-de5135aa36f9"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("546aeb4e-a7c3-4c53-a661-c2671b2b7a50"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("6d47bc81-81fc-4545-9c64-3c323aa9d260"));

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("715f967a-9ff9-4490-a6b2-567efa55c317"));

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "LogEventos",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "LogEventos",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Enderecos",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Emails",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Documentos",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER");

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
    }
}
