using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addingtimetoordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8fb0add1-e0bb-4f5a-9ad7-69564731f315"));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTime",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("28520ecc-3b3a-4b69-a22f-504495293acf"), "Created", null, null, false, null, "Created", true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("28520ecc-3b3a-4b69-a22f-504495293acf"));

            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("8fb0add1-e0bb-4f5a-9ad7-69564731f315"), "Created", null, null, false, null, "Created", true, null });
        }
    }
}
