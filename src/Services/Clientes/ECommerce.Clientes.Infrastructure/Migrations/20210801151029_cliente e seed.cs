using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class clienteeseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeFantasia",
                table: "Clientes",
                newName: "Sobrenome");

            migrationBuilder.AddColumn<DateTime>(
                name: "Nascimento",
                table: "Clientes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "Nascimento", "Nome", "Sobrenome" },
                values: new object[] { new Guid("236201b0-7f2e-4d85-aff6-8637c91d0215"), true, new DateTime(1955, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davi Giovanni Felipe", "Fernandes" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("236201b0-7f2e-4d85-aff6-8637c91d0215"));

            migrationBuilder.DropColumn(
                name: "Nascimento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "Clientes",
                newName: "NomeFantasia");
        }
    }
}
