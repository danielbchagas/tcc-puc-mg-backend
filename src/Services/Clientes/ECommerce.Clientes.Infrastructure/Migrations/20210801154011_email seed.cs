using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class emailseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Documentos");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ClienteId",
                table: "Emails",
                column: "ClienteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

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

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Enderecos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Documentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
