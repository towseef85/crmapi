using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class updatingUserTabletoadduserroleid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9f064a73-9fc2-4754-aeec-dd9640eafa73"));

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("0f138cb0-ece8-45ec-a079-74701c28dbf1"), "Created", null, null, false, null, "Created", true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0f138cb0-ece8-45ec-a079-74701c28dbf1"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("9f064a73-9fc2-4754-aeec-dd9640eafa73"), "Created", null, null, false, null, "Created", true, null });
        }
    }
}
