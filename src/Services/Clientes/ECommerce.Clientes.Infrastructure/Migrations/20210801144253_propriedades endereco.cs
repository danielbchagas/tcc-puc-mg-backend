using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class propriedadesendereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Enderecos",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Enderecos",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Enderecos",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Enderecos",
                type: "varchar(9)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Enderecos",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Enderecos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(9)");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
