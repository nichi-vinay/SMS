using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class TaxTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88cf0bf0-18ee-4433-9c20-cce9b8a46505");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7efc672-62a0-4874-ba13-f29ca6dcf665");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3cb6cc4-ba88-4a01-a6fa-456ab338d3a5");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TaxTypeMaster",
                newName: "TAXTypeId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c2ed5ab-897e-4f5e-8520-548937db110e", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54a52ebc-8a81-4793-9070-09ff3fdcbfea", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65bc46f4-6711-4fcf-b931-b2b75013fc3e", "2", "Executive", "Executive" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c2ed5ab-897e-4f5e-8520-548937db110e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54a52ebc-8a81-4793-9070-09ff3fdcbfea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65bc46f4-6711-4fcf-b931-b2b75013fc3e");

            migrationBuilder.RenameColumn(
                name: "TAXTypeId",
                table: "TaxTypeMaster",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88cf0bf0-18ee-4433-9c20-cce9b8a46505", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7efc672-62a0-4874-ba13-f29ca6dcf665", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3cb6cc4-ba88-4a01-a6fa-456ab338d3a5", "2", "Executive", "Executive" });
        }
    }
}
