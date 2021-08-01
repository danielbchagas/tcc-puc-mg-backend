using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class logdeeventos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "LogEventos",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "LogEventos",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "LogEventos",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "LogEventos",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");
        }
    }
}
