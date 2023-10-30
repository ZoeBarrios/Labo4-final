using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class cambiandoNombreCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_BuyerId",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "Purchases",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_BuyerId",
                table: "Purchases",
                newName: "IX_Purchases_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_UserId",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Purchases",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                newName: "IX_Purchases_BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_BuyerId",
                table: "Purchases",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
