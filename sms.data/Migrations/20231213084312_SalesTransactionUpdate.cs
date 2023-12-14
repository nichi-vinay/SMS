using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class SalesTransactionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseTransactionsMaster_PurchaseMaster_PurchasemasterId",
                table: "PurchaseTransactionsMaster");

            migrationBuilder.DropIndex(
                name: "IX_SalesTransactions_SalesmasterId",
                table: "SalesTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseTransactionsMaster",
                table: "PurchaseTransactionsMaster");

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

            migrationBuilder.RenameTable(
                name: "PurchaseTransactionsMaster",
                newName: "Purchasetransaction");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseTransactionsMaster_PurchasemasterId",
                table: "Purchasetransaction",
                newName: "IX_Purchasetransaction_PurchasemasterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchasetransaction",
                table: "Purchasetransaction",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cdce4d0-af5d-4d27-b1d9-5c3e8f6ca42c", "3", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2b7cbb5-142d-45e4-bd36-cb5e5c4be608", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2be401a-69c1-40d5-af89-d6c1bd907950", "2", "Executive", "Executive" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesTransactions_SalesmasterId",
                table: "SalesTransactions",
                column: "SalesmasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchasetransaction_PurchaseMaster_PurchasemasterId",
                table: "Purchasetransaction",
                column: "PurchasemasterId",
                principalTable: "PurchaseMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchasetransaction_PurchaseMaster_PurchasemasterId",
                table: "Purchasetransaction");

            migrationBuilder.DropIndex(
                name: "IX_SalesTransactions_SalesmasterId",
                table: "SalesTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchasetransaction",
                table: "Purchasetransaction");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cdce4d0-af5d-4d27-b1d9-5c3e8f6ca42c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2b7cbb5-142d-45e4-bd36-cb5e5c4be608");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2be401a-69c1-40d5-af89-d6c1bd907950");

            migrationBuilder.RenameTable(
                name: "Purchasetransaction",
                newName: "PurchaseTransactionsMaster");

            migrationBuilder.RenameIndex(
                name: "IX_Purchasetransaction_PurchasemasterId",
                table: "PurchaseTransactionsMaster",
                newName: "IX_PurchaseTransactionsMaster_PurchasemasterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseTransactionsMaster",
                table: "PurchaseTransactionsMaster",
                column: "Id");

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
                name: "IX_SalesTransactions_SalesmasterId",
                table: "SalesTransactions",
                column: "SalesmasterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseTransactionsMaster_PurchaseMaster_PurchasemasterId",
                table: "PurchaseTransactionsMaster",
                column: "PurchasemasterId",
                principalTable: "PurchaseMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
