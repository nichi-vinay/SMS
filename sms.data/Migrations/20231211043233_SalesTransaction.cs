using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sms.data.Migrations
{
    public partial class SalesTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Cards",
                table: "SalesMaster");

            migrationBuilder.DropColumn(
                name: "Cash",
                table: "SalesMaster");

            migrationBuilder.DropColumn(
                name: "Cheque",
                table: "SalesMaster");

            migrationBuilder.DropColumn(
                name: "Online",
                table: "SalesMaster");

            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "SalesMaster");

            migrationBuilder.CreateTable(
                name: "SalesTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesmasterId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SalesTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesTransactions_SalesMaster_SalesmasterId",
                        column: x => x.SalesmasterId,
                        principalTable: "SalesMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SalesTransactions_SalesmasterId",
                table: "SalesTransactions",
                column: "SalesmasterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesTransactions");

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

            migrationBuilder.AddColumn<float>(
                name: "Cards",
                table: "SalesMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Cash",
                table: "SalesMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Cheque",
                table: "SalesMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Online",
                table: "SalesMaster",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalPaid",
                table: "SalesMaster",
                type: "real",
                nullable: true);

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
    }
}
