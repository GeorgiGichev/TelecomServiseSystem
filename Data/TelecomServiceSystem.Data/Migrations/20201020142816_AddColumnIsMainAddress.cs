using Microsoft.EntityFrameworkCore.Migrations;

namespace TelecomServiceSystem.Data.Migrations
{
    public partial class AddColumnIsMainAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainAddress",
                table: "Addresses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainAddress",
                table: "Addresses");
        }
    }
}
