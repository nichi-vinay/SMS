using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class OrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "OrderItmeMaster");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "OrderMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "OrderItmeMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d86f05c-0af4-4c5f-8083-c0c43a727d14", "2", "Executive", "Executive" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91f340cf-3580-47fa-b8fe-4f4dc951cadc", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df8eb996-2d7d-4fc2-9f39-892f4dc08780", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d86f05c-0af4-4c5f-8083-c0c43a727d14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91f340cf-3580-47fa-b8fe-4f4dc951cadc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df8eb996-2d7d-4fc2-9f39-892f4dc08780");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "OrderMaster");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderItmeMaster");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "OrderItmeMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
