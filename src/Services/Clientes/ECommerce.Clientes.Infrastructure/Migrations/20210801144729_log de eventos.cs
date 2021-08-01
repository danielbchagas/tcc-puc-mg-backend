using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class logdeeventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Momento",
                table: "LogEventos",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Momento",
                table: "LogEventos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
