using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Basket.Infrastructure.Migrations
{
    public partial class basketended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnded",
                table: "Baskets",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnded",
                table: "Baskets");
        }
    }
}
