using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThemBangOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "OtpConfirms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtpCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpConfirms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("06816d0b-531b-4a7e-9809-f7adb478ea74"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("09794ef7-a0f4-4d91-b6cb-fee79847226d"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("3c9c515e-a1d1-4629-8fd5-62f2f3dca02d"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("3d6e98e5-4e27-4469-b819-7f456df48f72"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("481bc8fc-044e-441c-b9ab-12d813bd2469"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("52b897a6-28c9-488e-b682-8b99b6f21803"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("68687659-d9ba-4a0c-ad6b-1efe6d6bc1d5"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("6f8832cf-70a8-462b-aa5a-4be40ce4f760"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("9e7f33a2-0102-4067-965a-fc1e0cc9a06d"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("a6150f99-9f54-42b8-a948-530bf9433082"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("a6a2b337-be46-4e0c-9e58-93b77ef64e52"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("ccb6cf83-3cb2-4138-a8e3-006d73598298"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("ce593fe8-e3b1-4e8c-b698-e47352065f60"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("cf41d17b-1acb-4c03-8a55-b8dbc8fb6ca2"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$4FOQQKy8k9RZ9dLRnKcqHePC/eqDXlga9BM51KnvBJBpViOoNmDLO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$8mT9f04CjqzqIjplOzZoEuQb6OQ975kEcaVdEjiwY97/yjSxVn/cu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpConfirms");

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("06816d0b-531b-4a7e-9809-f7adb478ea74"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("09794ef7-a0f4-4d91-b6cb-fee79847226d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("3c9c515e-a1d1-4629-8fd5-62f2f3dca02d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("3d6e98e5-4e27-4469-b819-7f456df48f72"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("481bc8fc-044e-441c-b9ab-12d813bd2469"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("52b897a6-28c9-488e-b682-8b99b6f21803"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("68687659-d9ba-4a0c-ad6b-1efe6d6bc1d5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("6f8832cf-70a8-462b-aa5a-4be40ce4f760"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9e7f33a2-0102-4067-965a-fc1e0cc9a06d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a6150f99-9f54-42b8-a948-530bf9433082"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a6a2b337-be46-4e0c-9e58-93b77ef64e52"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ccb6cf83-3cb2-4138-a8e3-006d73598298"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ce593fe8-e3b1-4e8c-b698-e47352065f60"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("cf41d17b-1acb-4c03-8a55-b8dbc8fb6ca2"));

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
        }
    }
}
