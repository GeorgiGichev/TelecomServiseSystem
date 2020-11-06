namespace TelecomServiceSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddRelationOrderTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnginieringTaskId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OrderId",
                table: "Tasks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EnginieringTaskId",
                table: "Orders",
                column: "EnginieringTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tasks_EnginieringTaskId",
                table: "Orders",
                column: "EnginieringTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Orders_OrderId",
                table: "Tasks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tasks_EnginieringTaskId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Orders_OrderId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_OrderId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EnginieringTaskId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EnginieringTaskId",
                table: "Orders");
        }
    }
}
