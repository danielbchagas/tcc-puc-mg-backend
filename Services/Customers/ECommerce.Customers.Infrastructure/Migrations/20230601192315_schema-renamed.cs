using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Customers.Infrastructure.Migrations
{
    public partial class schemarenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.RenameTable(
                name: "Phones",
                schema: "Customers",
                newName: "Phones",
                newSchema: "Customer");

            migrationBuilder.RenameTable(
                name: "Emails",
                schema: "Customers",
                newName: "Emails",
                newSchema: "Customer");

            migrationBuilder.RenameTable(
                name: "Documents",
                schema: "Customers",
                newName: "Documents",
                newSchema: "Customer");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Customers",
                newName: "Customers",
                newSchema: "Customer");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "Customers",
                newName: "Addresses",
                newSchema: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customers");

            migrationBuilder.RenameTable(
                name: "Phones",
                schema: "Customer",
                newName: "Phones",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Emails",
                schema: "Customer",
                newName: "Emails",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Documents",
                schema: "Customer",
                newName: "Documents",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Customer",
                newName: "Customers",
                newSchema: "Customers");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "Customer",
                newName: "Addresses",
                newSchema: "Customers");
        }
    }
}
