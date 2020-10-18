using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelecomServiceSystem.Data.Migrations
{
    public partial class addDeleteColumnToSimAndDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SimCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "SimCards",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SimCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SimCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Devices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Devices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimCards_IsDeleted",
                table: "SimCards",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IsDeleted",
                table: "Devices",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SimCards_IsDeleted",
                table: "SimCards");

            migrationBuilder.DropIndex(
                name: "IX_Devices_IsDeleted",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SimCards");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "SimCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SimCards");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SimCards");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Devices");
        }
    }
}
