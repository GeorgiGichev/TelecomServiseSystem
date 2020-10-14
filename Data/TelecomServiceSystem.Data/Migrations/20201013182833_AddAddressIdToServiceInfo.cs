namespace TelecomServiceSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAddressIdToServiceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "ServicesInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInfos_AddressId",
                table: "ServicesInfos",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInfos_Addresses_AddressId",
                table: "ServicesInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInfos_Addresses_AddressId",
                table: "ServicesInfos");

            migrationBuilder.DropIndex(
                name: "IX_ServicesInfos_AddressId",
                table: "ServicesInfos");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "ServicesInfos");
        }
    }
}
