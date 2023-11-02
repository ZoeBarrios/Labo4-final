using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class cambiandonombresids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePublications_Publications_PublicationId",
                table: "PurchasePublications");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePublications_Purchases_PurchaseId",
                table: "PurchasePublications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePublications",
                table: "PurchasePublications");

            migrationBuilder.RenameTable(
                name: "PurchasePublications",
                newName: "PurchasePublication");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Purchases",
                newName: "PurchaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Publications",
                newName: "PublicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasePublications_PurchaseId",
                table: "PurchasePublication",
                newName: "IX_PurchasePublication_PurchaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePublication",
                table: "PurchasePublication",
                columns: new[] { "PublicationId", "PurchaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePublication_Publications_PublicationId",
                table: "PurchasePublication",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePublication_Purchases_PurchaseId",
                table: "PurchasePublication",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePublication_Publications_PublicationId",
                table: "PurchasePublication");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePublication_Purchases_PurchaseId",
                table: "PurchasePublication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePublication",
                table: "PurchasePublication");

            migrationBuilder.RenameTable(
                name: "PurchasePublication",
                newName: "PurchasePublications");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Purchases",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PublicationId",
                table: "Publications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasePublication_PurchaseId",
                table: "PurchasePublications",
                newName: "IX_PurchasePublications_PurchaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePublications",
                table: "PurchasePublications",
                columns: new[] { "PublicationId", "PurchaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePublications_Publications_PublicationId",
                table: "PurchasePublications",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePublications_Purchases_PurchaseId",
                table: "PurchasePublications",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
