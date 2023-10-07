using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addingnewcolumnstoorderandorderrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("23887340-3e84-4e4c-9025-3a151a3f5427"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderRequestId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OrderDone",
                table: "OrderRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("0df43d3b-ec66-40a5-bbc3-80232b981506"), "Created", null, null, false, null, "Created", true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0df43d3b-ec66-40a5-bbc3-80232b981506"));

            migrationBuilder.DropColumn(
                name: "OrderRequestId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDone",
                table: "OrderRequests");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("23887340-3e84-4e4c-9025-3a151a3f5427"), "Created", null, null, false, null, "Created", true, null });
        }
    }
}
