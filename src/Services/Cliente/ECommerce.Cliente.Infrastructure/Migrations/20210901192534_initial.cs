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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", nullable: false),
                    Estado = table.Column<int>(type: "char(2)", nullable: false),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                values: new object[] { new Guid("507f941f-9a2e-4031-bead-4dd89d84a4ba"), true, "Davi Giovanni Felipe", "Fernandes" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nome", "Sobrenome" },
                values: new object[] { new Guid("101e2a8c-e88a-4127-875b-fbc05f183edf"), true, "Ayla Caroline", "Ana Gomes" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nome", "Sobrenome" },
                values: new object[] { new Guid("662069b0-a554-4589-ac61-c782da525625"), true, "BetinaFlávia", "Souza" });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[] { new Guid("12c637f6-9f2d-479e-a891-323abb615975"), new Guid("507f941f-9a2e-4031-bead-4dd89d84a4ba"), "903.142.734-92" });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[] { new Guid("0fe25255-ff1a-4ebc-a139-3dfed13b3258"), new Guid("101e2a8c-e88a-4127-875b-fbc05f183edf"), "668.154.787-77" });

            migrationBuilder.InsertData(
                table: "Documentos",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[] { new Guid("66968d02-cef1-4584-bb94-dbc57cc6fb20"), new Guid("662069b0-a554-4589-ac61-c782da525625"), "345.712.047-10" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[] { new Guid("bc8170e0-66cd-49e2-bdd4-0752f4ab7d65"), new Guid("507f941f-9a2e-4031-bead-4dd89d84a4ba"), "davi_giovanni_felipe@gmail.com" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[] { new Guid("02ded591-029f-4b90-b13f-f7ff6274bb73"), new Guid("101e2a8c-e88a-4127-875b-fbc05f183edf"), "ayla_caroline_ana_gomes@gmail.com" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "ClienteId", "Endereco" },
                values: new object[] { new Guid("1308e91e-3c41-45bc-a77d-31572749b95e"), new Guid("662069b0-a554-4589-ac61-c782da525625"), "b_etina_flavia_souza@gmail.com" });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[] { new Guid("999e2ba3-9a81-4674-8b07-9c488b1f2da4"), "Guará I", "71090-265", "Brasília", new Guid("507f941f-9a2e-4031-bead-4dd89d84a4ba"), 6, "Colônia Agrícola Águas Claras Chácara 23, 641" });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[] { new Guid("996b46b1-839f-4d7a-940f-4294fdd60f15"), "Tarumã", "82530-220", "Curitiba", new Guid("101e2a8c-e88a-4127-875b-fbc05f183edf"), 15, "Praça São Francisco de Assis, 442" });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "ClienteId", "Estado", "Logradouro" },
                values: new object[] { new Guid("1ee8de2d-2f9f-4158-8d7a-1ef008b3d2af"), "Abegay", "98045-115", "Cruz Alta", new Guid("662069b0-a554-4589-ac61-c782da525625"), 20, "Rua Neves, 378" });

            migrationBuilder.InsertData(
                table: "Telefones",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[] { new Guid("62936aaf-9563-45a1-bdcb-84957e54e23b"), new Guid("507f941f-9a2e-4031-bead-4dd89d84a4ba"), "(82) 98621-8773" });

            migrationBuilder.InsertData(
                table: "Telefones",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[] { new Guid("291b77b6-179c-4531-bb3e-c253a8c372e1"), new Guid("101e2a8c-e88a-4127-875b-fbc05f183edf"), "(91) 98965-5955" });

            migrationBuilder.InsertData(
                table: "Telefones",
                columns: new[] { "Id", "ClienteId", "Numero" },
                values: new object[] { new Guid("098db767-6609-4278-ae45-df3a1308de89"), new Guid("662069b0-a554-4589-ac61-c782da525625"), "(95) 98234-7636" });

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
                name: "Telefones");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
