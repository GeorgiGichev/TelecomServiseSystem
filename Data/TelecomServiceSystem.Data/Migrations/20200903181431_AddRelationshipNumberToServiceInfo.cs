namespace TelecomServiceSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddRelationshipNumberToServiceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "ServicesInfos");

            migrationBuilder.AddColumn<int>(
                name: "ServiceNumberId",
                table: "ServicesInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInfos_ServiceNumberId",
                table: "ServicesInfos",
                column: "ServiceNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInfos_ServiceNumbers_ServiceNumberId",
                table: "ServicesInfos",
                column: "ServiceNumberId",
                principalTable: "ServiceNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInfos_ServiceNumbers_ServiceNumberId",
                table: "ServicesInfos");

            migrationBuilder.DropIndex(
                name: "IX_ServicesInfos_ServiceNumberId",
                table: "ServicesInfos");

            migrationBuilder.DropColumn(
                name: "ServiceNumberId",
                table: "ServicesInfos");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "ServicesInfos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
