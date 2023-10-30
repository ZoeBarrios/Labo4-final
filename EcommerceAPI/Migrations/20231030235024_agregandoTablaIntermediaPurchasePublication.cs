using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class agregandoTablaIntermediaPurchasePublication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Publications_PublicationId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_PublicationId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Purchases");

            migrationBuilder.CreateTable(
                name: "PurchasePublication",
                columns: table => new
                {
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePublication", x => new { x.PublicationId, x.PurchaseId });
                    table.ForeignKey(
                        name: "FK_PurchasePublication_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasePublication_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePublication_PurchaseId",
                table: "PurchasePublication",
                column: "PurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasePublication");

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PublicationId",
                table: "Purchases",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Publications_PublicationId",
                table: "Purchases",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
