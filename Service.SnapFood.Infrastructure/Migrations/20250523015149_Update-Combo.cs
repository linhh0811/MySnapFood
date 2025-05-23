using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCombo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreteDate",
                table: "Combos");

            migrationBuilder.AddColumn<string>(
                name: "Numberphone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Combos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numberphone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Combos");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreteDate",
                table: "Combos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
