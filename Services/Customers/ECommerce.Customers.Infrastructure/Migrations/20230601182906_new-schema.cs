using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Customers.Infrastructure.Migrations
{
    public partial class newschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customers");

            migrationBuilder.RenameTable(
                name: "Phones",
                newName: "Phones",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Emails",
                newName: "Emails",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Documents",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Addresses",
                newSchema: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Phones",
                schema: "Customers",
                newName: "Phones");

            migrationBuilder.RenameTable(
                name: "Emails",
                schema: "Customers",
                newName: "Emails");

            migrationBuilder.RenameTable(
                name: "Documents",
                schema: "Customers",
                newName: "Documents");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Customers",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "Customers",
                newName: "Addresses");
        }
    }
}
