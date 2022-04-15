using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Identity.Api.Migrations
{
    public partial class email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "f83a754f-ba51-43a0-90b2-fd135ac921b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5EB7CD58-2775-48A5-8E6B-BC935C582222",
                column: "ConcurrencyStamp",
                value: "144c0ee7-757d-4f35-8a2c-8577047e54fa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8fc883e7-bc97-4b49-aac4-5ddc14d60962", "admin@ecommerce.com", "ADMIN@ECOMMERCE.COM", "ADMIN@ECOMMERCE.COM", "AQAAAAEAACcQAAAAEBRnhcXHhAKHTHVYwD7sxkrGv5dggUFj8SbWQIUU1AVYER32xT9JECTTbcaYA5lCfA==", "302a2c81-d1c8-44c8-8419-4e5a45d38aaa", "admin@ecommerce.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0c5dcb0b-1ce5-4cc9-b052-1824fe3ee51c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5EB7CD58-2775-48A5-8E6B-BC935C582222",
                column: "ConcurrencyStamp",
                value: "5de7bf49-94dc-40a0-93fb-a3d527373e4f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "c5d4a6b0-09f4-431c-9e44-321f69336271", null, null, "ADMIN", "AQAAAAEAACcQAAAAEGE/5qvKC0d9BZu0zmvB7f78y16AFXfdx/+g9A4x4Lk1qWzNPyCNXV5F7tSwcH+DcQ==", "c94642ee-a7d0-4023-a4d5-352691308517", "admin" });
        }
    }
}
