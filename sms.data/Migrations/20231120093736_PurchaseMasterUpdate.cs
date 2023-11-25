using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class PurchaseMasterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05067fdf-26bb-4dca-9d3c-ae454a57f1f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a220ddb0-2874-463a-8f54-7402c50ab3cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f74ef0a9-7dcb-4910-a6b7-b4ce2b96656e");

            migrationBuilder.AddColumn<float>(
                name: "Cards",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Cash",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Cheque",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Online",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalAmount",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalDiscount",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalPaid",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalTax",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d031d18-6019-4969-bb6b-619bc034ab20", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14843b5-cc0c-4cd4-9764-7db8efcfe6c8", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4e3bcbd-94fc-4b28-9a57-96a30db77f02", "2", "Executive", "Executive" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d031d18-6019-4969-bb6b-619bc034ab20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14843b5-cc0c-4cd4-9764-7db8efcfe6c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4e3bcbd-94fc-4b28-9a57-96a30db77f02");

            migrationBuilder.DropColumn(
                name: "Cards",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "Cash",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "Cheque",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "Online",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "PurchaseMaster");

            migrationBuilder.DropColumn(
                name: "TotalTax",
                table: "PurchaseMaster");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05067fdf-26bb-4dca-9d3c-ae454a57f1f8", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a220ddb0-2874-463a-8f54-7402c50ab3cb", "2", "Executive", "Executive" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f74ef0a9-7dcb-4910-a6b7-b4ce2b96656e", "3", "User", "User" });
        }
    }
}
