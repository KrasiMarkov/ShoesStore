using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoesStore.Data.Migrations
{
    public partial class AddShoeIdColumnInOrderDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoeId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ShoeId",
                table: "OrderDetails",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Shoes_ShoeId",
                table: "OrderDetails",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Shoes_ShoeId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ShoeId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShoeId",
                table: "OrderDetails");
        }
    }
}
