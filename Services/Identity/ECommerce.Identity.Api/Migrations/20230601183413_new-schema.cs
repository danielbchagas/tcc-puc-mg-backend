using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Identity.Api.Migrations
{
    public partial class newschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "Identity");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "72b66e9b-012e-4ee6-b376-9293747bf560");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5EB7CD58-2775-48A5-8E6B-BC935C582222",
                column: "ConcurrencyStamp",
                value: "f1d415dc-8141-41d5-aa36-ac1e47455826");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06faed8f-9a25-4883-9eb1-914cec3b723c", "AQAAAAEAACcQAAAAEAkmPXyXRPogc6qEMvcZEILsomgr03Y4eLZltP2jXYX76BNetasUGgPo9AEdD1RKUw==", "0162beb2-c02a-4b5b-82ef-4f136b32c88d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "Identity",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "Identity",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "Identity",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "Identity",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "Identity",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "Identity",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "Identity",
                newName: "AspNetRoleClaims");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7bcb0676-b2a8-48ca-bb41-6677fe959ce9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5EB7CD58-2775-48A5-8E6B-BC935C582222",
                column: "ConcurrencyStamp",
                value: "a973ec2c-d3dd-454f-a2ec-9530610b3068");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "097cfcc1-4335-443e-9080-7f1c14001dab", "AQAAAAEAACcQAAAAEDd6l4TinmHDPbmTNUc9jrMwtcX3Kr+KxjHhW28E2EAEnQqGq3uD4Y8YMY0rhbrsOQ==", "9061e241-688e-42c7-853a-eb53324c08ea" });
        }
    }
}
