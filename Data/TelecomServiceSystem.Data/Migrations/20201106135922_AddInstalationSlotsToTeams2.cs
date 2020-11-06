namespace TelecomServiceSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddInstalationSlotsToTeams2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalationSlot_Teams_TeamId",
                table: "InstalationSlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstalationSlot",
                table: "InstalationSlot");

            migrationBuilder.RenameTable(
                name: "InstalationSlot",
                newName: "InstalationSlots");

            migrationBuilder.RenameIndex(
                name: "IX_InstalationSlot_TeamId",
                table: "InstalationSlots",
                newName: "IX_InstalationSlots_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_InstalationSlot_IsDeleted",
                table: "InstalationSlots",
                newName: "IX_InstalationSlots_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstalationSlots",
                table: "InstalationSlots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalationSlots_Teams_TeamId",
                table: "InstalationSlots",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalationSlots_Teams_TeamId",
                table: "InstalationSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstalationSlots",
                table: "InstalationSlots");

            migrationBuilder.RenameTable(
                name: "InstalationSlots",
                newName: "InstalationSlot");

            migrationBuilder.RenameIndex(
                name: "IX_InstalationSlots_TeamId",
                table: "InstalationSlot",
                newName: "IX_InstalationSlot_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_InstalationSlots_IsDeleted",
                table: "InstalationSlot",
                newName: "IX_InstalationSlot_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstalationSlot",
                table: "InstalationSlot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalationSlot_Teams_TeamId",
                table: "InstalationSlot",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
