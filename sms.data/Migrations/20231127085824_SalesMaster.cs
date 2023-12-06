using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class SalesMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d251cb5b-066b-4212-bd49-826b24f19be7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e702646f-f009-404d-a483-ad325edab841");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6626e3d-771b-43d9-ba4f-04b29da2e1ce");

            migrationBuilder.AddColumn<float>(
                name: "TotalMrp",
                table: "SalesMaster",
                type: "real",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TotalMrp",
                table: "SalesMaster");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d251cb5b-066b-4212-bd49-826b24f19be7", "2", "Executive", "Executive" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e702646f-f009-404d-a483-ad325edab841", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f6626e3d-771b-43d9-ba4f-04b29da2e1ce", "3", "User", "User" });
        }
    }
}
