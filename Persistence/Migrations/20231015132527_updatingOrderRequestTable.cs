using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class updatingOrderRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0f138cb0-ece8-45ec-a079-74701c28dbf1"));

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "OrderRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("8fb0add1-e0bb-4f5a-9ad7-69564731f315"), "Created", null, null, false, null, "Created", true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8fb0add1-e0bb-4f5a-9ad7-69564731f315"));

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "OrderRequests");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("0f138cb0-ece8-45ec-a079-74701c28dbf1"), "Created", null, null, false, null, "Created", true, null });
        }
    }
}
