using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class workedonorderrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0df43d3b-ec66-40a5-bbc3-80232b981506"));

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("59515252-e130-4d76-a241-17d765ba7cdb"), "Created", null, null, false, null, "Created", true, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("59515252-e130-4d76-a241-17d765ba7cdb"));

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "ArbName", "CreatedDate", "DeleteDate", "Deleted", "Description", "EngName", "IsActive", "UpdatedDate" },
                values: new object[] { new Guid("0df43d3b-ec66-40a5-bbc3-80232b981506"), "Created", null, null, false, null, "Created", true, null });
        }
    }
}
