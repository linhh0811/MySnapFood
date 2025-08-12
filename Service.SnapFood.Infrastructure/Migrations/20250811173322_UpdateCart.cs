using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0505c6fb-49af-4357-affc-a4588952a5bf"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("19285271-92c9-42fd-b79b-030fadc8f1a6"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("32f30f08-aa66-4763-b58e-92f0eaaa2127"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("59c83831-3705-4afa-b9c8-6c05638ce1f1"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7a3e5f78-0735-4faa-b2a6-5904d2e5673b"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7a9066af-ec90-4cb3-97b7-862105926ad0"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7de86e5f-f1f4-4914-b365-9b2ae41fc329"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("966357cc-f815-44c7-9201-62678712aecd"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c759da94-d94b-4fc6-85f6-ebe4fd5883a0"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ca5b7d7e-5965-4076-b7c0-c8ac39958494"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d11c35f9-1005-4267-bc7c-0f6c34fb7098"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e577a5a5-b53d-4d84-96f9-6ce3c9d557b4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("edc0e8d6-a11b-4c0a-bc79-ae2381104516"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f25982a9-d96f-40d8-8021-96201dd2d69a"));

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0310b8d1-ec56-4a77-8256-060dcab48696"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("161593e4-dd84-4bd9-a3e7-5f6ecb9f0bf5"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("1a6283f0-b450-4adc-ac3a-41c48c38a8f3"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("1c9c3182-9a4d-4a30-93a0-8116c1e7e152"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("1d1d7028-b764-4168-8861-f2d04dc027ee"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("4955879c-efdd-4a56-8f70-9aa6e72d56c4"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("5570a0ea-dfb2-4f1e-bbe7-fc248dfdf703"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("70aad817-6a13-4b06-8e20-2cfa77990f51"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("9f3f5756-263e-446e-9f61-c8b6ab4e2647"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("a0051ac7-834c-4634-85ca-002f45f41852"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("a3a7bd50-ea43-4a4f-ace4-0dbf2f6a6287"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("b468cdc9-48ee-4b06-8801-55ee4a33e9e5"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("c6c73b17-add4-42c6-b580-fd98abc770af"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("e8d90341-f623-46db-905c-f8109ae9fb75"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$SG7oq1X9d9KPDUDnjJi/b.1VsKkLk.B2s9ajRZyVJJr/26YJzCBAW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$Gx1U/sKywz56.lFnvpdXa.w9dWV21co/7hzFRiVG9rHxAfcO3008C");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0310b8d1-ec56-4a77-8256-060dcab48696"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("161593e4-dd84-4bd9-a3e7-5f6ecb9f0bf5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1a6283f0-b450-4adc-ac3a-41c48c38a8f3"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1c9c3182-9a4d-4a30-93a0-8116c1e7e152"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1d1d7028-b764-4168-8861-f2d04dc027ee"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("4955879c-efdd-4a56-8f70-9aa6e72d56c4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("5570a0ea-dfb2-4f1e-bbe7-fc248dfdf703"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("70aad817-6a13-4b06-8e20-2cfa77990f51"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9f3f5756-263e-446e-9f61-c8b6ab4e2647"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a0051ac7-834c-4634-85ca-002f45f41852"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a3a7bd50-ea43-4a4f-ace4-0dbf2f6a6287"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b468cdc9-48ee-4b06-8801-55ee4a33e9e5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c6c73b17-add4-42c6-b580-fd98abc770af"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e8d90341-f623-46db-905c-f8109ae9fb75"));

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0505c6fb-49af-4357-affc-a4588952a5bf"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("19285271-92c9-42fd-b79b-030fadc8f1a6"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("32f30f08-aa66-4763-b58e-92f0eaaa2127"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("59c83831-3705-4afa-b9c8-6c05638ce1f1"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("7a3e5f78-0735-4faa-b2a6-5904d2e5673b"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("7a9066af-ec90-4cb3-97b7-862105926ad0"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("7de86e5f-f1f4-4914-b365-9b2ae41fc329"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("966357cc-f815-44c7-9201-62678712aecd"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("c759da94-d94b-4fc6-85f6-ebe4fd5883a0"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("ca5b7d7e-5965-4076-b7c0-c8ac39958494"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("d11c35f9-1005-4267-bc7c-0f6c34fb7098"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("e577a5a5-b53d-4d84-96f9-6ce3c9d557b4"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("edc0e8d6-a11b-4c0a-bc79-ae2381104516"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("f25982a9-d96f-40d8-8021-96201dd2d69a"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$yEzbuxRF0rL0r6xQcNSXs.17IT045Ol2semtshuO6UJa5HQAujxtq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$eQwBK96UBHdh.j0VgOzkyOmim50hA9vWuipht2zTPbwcRPxPJAV/W");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);
        }
    }
}
