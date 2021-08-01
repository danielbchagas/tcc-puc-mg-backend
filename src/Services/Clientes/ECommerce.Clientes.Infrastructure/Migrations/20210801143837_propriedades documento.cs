using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Clientes.Infrastructure.Migrations
{
    public partial class propriedadesdocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Clientes_ClienteId",
                table: "Documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documento",
                table: "Documento");

            migrationBuilder.RenameTable(
                name: "Documento",
                newName: "Documentos");

            migrationBuilder.RenameIndex(
                name: "IX_Documento_ClienteId",
                table: "Documentos",
                newName: "IX_Documentos_ClienteId");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Documentos",
                type: "varchar(18)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documentos",
                table: "Documentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Clientes_ClienteId",
                table: "Documentos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Clientes_ClienteId",
                table: "Documentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documentos",
                table: "Documentos");

            migrationBuilder.RenameTable(
                name: "Documentos",
                newName: "Documento");

            migrationBuilder.RenameIndex(
                name: "IX_Documentos_ClienteId",
                table: "Documento",
                newName: "IX_Documento_ClienteId");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Documento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(18)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documento",
                table: "Documento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Clientes_ClienteId",
                table: "Documento",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
