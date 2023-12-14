using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class PurchaseTransactionMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14aea3bf-b1fa-4815-b265-df8b19a59f4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52d25c94-0132-438c-a96e-d32271fd898e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2576e6c-b3c1-46cd-9b4d-c77ab51c2653");

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
                name: "TotalPaid",
                table: "PurchaseMaster");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "SalesItemMaster",
                newName: "TaxPercentage");

            migrationBuilder.CreateTable(
                name: "PurchaseTransactionsMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchasemasterId = table.Column<int>(type: "int", nullable: false),
                    Cheque = table.Column<float>(type: "real", nullable: true),
                    Cash = table.Column<float>(type: "real", nullable: true),
                    Online = table.Column<float>(type: "real", nullable: true),
                    Cards = table.Column<float>(type: "real", nullable: true),
                    TotalPaid = table.Column<float>(type: "real", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTransactionsMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseTransactionsMaster_PurchaseMaster_PurchasemasterId",
                        column: x => x.PurchasemasterId,
                        principalTable: "PurchaseMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2565cd82-38b6-422f-9a57-8ef0b8e92fec", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "753b587d-ed4a-4abe-a14d-01ca6ec76eff", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75d14cd9-6510-4135-908b-b550875383d5", "2", "Executive", "Executive" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTransactionsMaster_PurchasemasterId",
                table: "PurchaseTransactionsMaster",
                column: "PurchasemasterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseTransactionsMaster");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2565cd82-38b6-422f-9a57-8ef0b8e92fec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "753b587d-ed4a-4abe-a14d-01ca6ec76eff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75d14cd9-6510-4135-908b-b550875383d5");

            migrationBuilder.RenameColumn(
                name: "TaxPercentage",
                table: "SalesItemMaster",
                newName: "DiscountPercentage");

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
                name: "TotalPaid",
                table: "PurchaseMaster",
                type: "real",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14aea3bf-b1fa-4815-b265-df8b19a59f4a", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52d25c94-0132-438c-a96e-d32271fd898e", "2", "Executive", "Executive" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2576e6c-b3c1-46cd-9b4d-c77ab51c2653", "3", "User", "User" });
        }
    }
}
