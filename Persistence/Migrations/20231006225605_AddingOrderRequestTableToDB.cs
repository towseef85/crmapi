using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddingOrderRequestTableToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("2ae7a833-eb2f-4046-b3bc-4149fab0e47b"));

            migrationBuilder.CreateTable(
                name: "OrderRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryType = table.Column<int>(type: "int", nullable: false),
                    CODCharges = table.Column<float>(type: "real", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderAmount = table.Column<float>(type: "real", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderRequests_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("23887340-3e84-4e4c-9025-3a151a3f5427"), "Created", null, null, false, null, "Created", true, null });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequests_VendorId",
                table: "OrderRequests",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRequests");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("23887340-3e84-4e4c-9025-3a151a3f5427"));

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("2ae7a833-eb2f-4046-b3bc-4149fab0e47b"), "Created", null, null, false, null, "Created", true, null });
        }
    }
}
