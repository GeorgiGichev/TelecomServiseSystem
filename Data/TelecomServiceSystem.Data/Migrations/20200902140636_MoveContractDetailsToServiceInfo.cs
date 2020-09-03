namespace TelecomServiceSystem.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MoveContractDetailsToServiceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDuration",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Expirеs",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ContractDuration",
                table: "ServicesInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Expirеs",
                table: "ServicesInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IMEI",
                table: "ServicesInfos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ServicesInfos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDuration",
                table: "ServicesInfos");

            migrationBuilder.DropColumn(
                name: "Expirеs",
                table: "ServicesInfos");

            migrationBuilder.DropColumn(
                name: "IMEI",
                table: "ServicesInfos");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ServicesInfos");

            migrationBuilder.AddColumn<int>(
                name: "ContractDuration",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Expirеs",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
