using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addingmorecolumnstoordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ExtraCharges",
                table: "Orders",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraCharges",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Orders");
        }
    }
}
