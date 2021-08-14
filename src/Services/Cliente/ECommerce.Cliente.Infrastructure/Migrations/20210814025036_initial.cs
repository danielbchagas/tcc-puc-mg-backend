using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Cliente.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Momento = table.Column<DateTime>(type: "date", nullable: false),
                    EntidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "varchar(18)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", nullable: false),
                    Estado = table.Column<string>(type: "char(2)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nome", "Sobrenome" },
                values: new object[] { new Guid("bf80711d-72fd-4fe6-899f-8e797a695c49"), true, "Davi Giovanni Felipe", "Fernandes" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nome", "Sobrenome" },
                values: new object[] { new Guid("d041eb65-2e2c-4f35-9cce-545070fbdc23"), true, "Ayla Caroline", "Ana Gomes" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nome", "Sobrenome" },
                values: new object[] { new Guid("38da0b16-6382-43b7-93ee-c53bcd0c5b15"), true, "BetinaFlávia", "Souza" });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("b4ed5c41-3987-44c1-b134-33fd6d675554"), new Guid("bf80711d-72fd-4fe6-899f-8e797a695c49"), "903.142.734-92" },
                    { new Guid("3ecaf44f-103a-4c02-9f7b-c665e4ceefec"), new Guid("d041eb65-2e2c-4f35-9cce-545070fbdc23"), "668.154.787-77" },
                    { new Guid("fb0a21dd-8ee5-4516-a8e4-8de4b4cc81f8"), new Guid("38da0b16-6382-43b7-93ee-c53bcd0c5b15"), "345.712.047-10" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[,]
                {
                    { new Guid("32090bdc-16dc-4d2e-b411-23d32c742fe5"), new Guid("bf80711d-72fd-4fe6-899f-8e797a695c49"), "davi_giovanni_felipe@gmail.com" },
                    { new Guid("b9bd1049-490f-4e44-942c-2e9deaba69cf"), new Guid("d041eb65-2e2c-4f35-9cce-545070fbdc23"), "ayla_caroline_ana_gomes@gmail.com" },
                    { new Guid("e044c872-92d7-4824-98f3-14a0f720ae56"), new Guid("38da0b16-6382-43b7-93ee-c53bcd0c5b15"), "b_etina_flavia_souza@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[,]
                {
                    { new Guid("d525101d-8095-4cc7-8f80-670c0ccdb3c9"), "Guará I", "71090-265", "Brasília", new Guid("bf80711d-72fd-4fe6-899f-8e797a695c49"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" },
                    { new Guid("b8d83ba3-a29a-489c-a392-1236066c6803"), "Tarumã", "82530-220", "Curitiba", new Guid("d041eb65-2e2c-4f35-9cce-545070fbdc23"), "PR", "Praça São Francisco de Assis, 442" },
                    { new Guid("82e2f473-cf31-420a-9c17-01c71ff5221c"), "Abegay", "98045-115", "Cruz Alta", new Guid("38da0b16-6382-43b7-93ee-c53bcd0c5b15"), "RS", "Rua Neves, 378" }
                });

            migrationBuilder.InsertData(
                table: "Telefones",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("2d8c132d-db4d-4e1e-beb3-c9d0af5ed2c6"), new Guid("bf80711d-72fd-4fe6-899f-8e797a695c49"), "(82) 98621-8773" },
                    { new Guid("b872ea47-f005-4b5f-89c6-56473a809d5e"), new Guid("d041eb65-2e2c-4f35-9cce-545070fbdc23"), "(91) 98965-5955" },
                    { new Guid("dbc3c525-d813-4471-9c76-87f5109ecbfe"), new Guid("38da0b16-6382-43b7-93ee-c53bcd0c5b15"), "(95) 98234-7636" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ClienteId",
                table: "Documentos",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ClienteId",
                table: "Emails",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_ClienteId",
                table: "Telefones",
                column: "ClienteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "LogEventos");

            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
