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
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
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
                columns: new[] { "Id", "Ativo", "DataNascimento", "Nome", "Sobrenome" },
                values: new object[] { new Guid("29ac2961-6aaf-4e62-bee7-036f2e0c163a"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "DataNascimento", "Nome", "Sobrenome" },
                values: new object[] { new Guid("d84f5433-b284-46ba-872d-dd82136a4ae2"), true, new DateTime(1963, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayla Caroline", "Ana Gomes" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "DataNascimento", "Nome", "Sobrenome" },
                values: new object[] { new Guid("ff1c5c76-a8d9-46f8-9d3d-ffd0fd002742"), true, new DateTime(1975, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "BetinaFlávia", "Souza" });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("a491b5e9-d04c-4018-a2e9-72c6cbc00835"), new Guid("29ac2961-6aaf-4e62-bee7-036f2e0c163a"), "903.142.734-92" },
                    { new Guid("245f234e-0aa8-43b4-af33-3c43f7c5335e"), new Guid("d84f5433-b284-46ba-872d-dd82136a4ae2"), "668.154.787-77" },
                    { new Guid("c69b1337-777e-4aa4-9834-de33e1bd2d03"), new Guid("ff1c5c76-a8d9-46f8-9d3d-ffd0fd002742"), "345.712.047-10" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[,]
                {
                    { new Guid("2d43686f-a66a-40e2-8f98-09831f48be79"), new Guid("29ac2961-6aaf-4e62-bee7-036f2e0c163a"), "davi_giovanni_felipe@gmail.com" },
                    { new Guid("323afdd7-2c5b-4810-a687-c8fd64a274c4"), new Guid("d84f5433-b284-46ba-872d-dd82136a4ae2"), "ayla_caroline_ana_gomes@gmail.com" },
                    { new Guid("6eac47da-df74-462c-9a8c-f35065e689ab"), new Guid("ff1c5c76-a8d9-46f8-9d3d-ffd0fd002742"), "b_etina_flavia_souza@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[,]
                {
                    { new Guid("75f6e8e4-a189-49d3-b666-9ed98fefab78"), "Guará I", "71090-265", "Brasília", new Guid("29ac2961-6aaf-4e62-bee7-036f2e0c163a"), "DF", "Colônia Agrícola Águas Claras Chácara 23, 641" },
                    { new Guid("b1e767a3-c6ec-4540-96c8-95a6b3f6f1ad"), "Tarumã", "82530-220", "Curitiba", new Guid("d84f5433-b284-46ba-872d-dd82136a4ae2"), "PR", "Praça São Francisco de Assis, 442" },
                    { new Guid("766da776-8a74-489c-b5e1-0cfe69ad4a57"), "Abegay", "98045-115", "Cruz Alta", new Guid("ff1c5c76-a8d9-46f8-9d3d-ffd0fd002742"), "RS", "Rua Neves, 378" }
                });

            migrationBuilder.InsertData(
                table: "Telefones",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[,]
                {
                    { new Guid("8ceff80e-2bdb-4658-93a7-f2bbf15ea412"), new Guid("29ac2961-6aaf-4e62-bee7-036f2e0c163a"), "(82) 98621-8773" },
                    { new Guid("11ac5fcd-a436-43cb-b9df-1a444ffc7228"), new Guid("d84f5433-b284-46ba-872d-dd82136a4ae2"), "(91) 98965-5955" },
                    { new Guid("e544eb91-cff6-45a1-a825-318e079fd096"), new Guid("ff1c5c76-a8d9-46f8-9d3d-ffd0fd002742"), "(95) 98234-7636" }
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
