using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class modificandomodelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameIndex(
                name: "IX_PurchasePublication_PurchaseId",
                table: "PurchasePublications",
                newName: "IX_PurchasePublications_PurchaseId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "PurchasePublications",
                newName: "PurchasePublication");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePublication_Purchases_PurchaseId",
                table: "PurchasePublication",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
