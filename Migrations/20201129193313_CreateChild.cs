using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerProject.Migrations
{
    public partial class CreateChild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42d1aff8-73b9-4fde-95dc-f2cf93121514");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caf86ccf-29d4-4f4f-bedf-14b0db775c5c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e7c3543-72c1-45a0-9d0d-a72d3a3c8241", "0edcb1c4-f2ac-4213-a1cc-b852dfe67173", "Parent", "PARENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0165ae54-c32f-45cb-80b8-19f85e522bfa", "e7d4612e-04be-4fe1-955f-722879f3288f", "Child", "CHILD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0165ae54-c32f-45cb-80b8-19f85e522bfa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e7c3543-72c1-45a0-9d0d-a72d3a3c8241");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42d1aff8-73b9-4fde-95dc-f2cf93121514", "bdf13397-6e23-463f-b0a2-9f29fe001e57", "Parent", "PARENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "caf86ccf-29d4-4f4f-bedf-14b0db775c5c", "6090b9aa-6daf-4f94-b9b2-eb5cefa180a4", "Child", "CHILD" });
        }
    }
}
