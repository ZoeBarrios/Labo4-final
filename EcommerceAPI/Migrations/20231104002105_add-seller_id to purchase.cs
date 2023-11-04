using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class addseller_idtopurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SellerId",
                table: "Purchases",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_SellerId",
                table: "Purchases",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_SellerId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SellerId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Purchases");
        }
    }
}
