using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateConfigProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId",
                unique: true,
                filter: "[SizeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos",
                column: "CategoryId",
                unique: true);
        }
    }
}
