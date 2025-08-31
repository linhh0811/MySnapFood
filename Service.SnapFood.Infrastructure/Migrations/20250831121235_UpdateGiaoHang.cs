using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGiaoHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0c714975-9863-45f1-9916-fa1466a73586"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("19e3c551-1f82-4df8-a1af-ce1d591dffe5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("232348f3-5b7d-46b2-9f58-9ac101e3208f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("39277d63-2c5e-48c3-8f29-4c54a1ed4ef3"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("5d98ee2d-42a0-4413-be1f-d3d19b79582a"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("6e4a7329-c091-4c94-a92d-d3707bec8438"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("71766ff7-6242-4508-ac08-a683ec343c52"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("89a1745e-d614-4b22-a0c3-0f0521f70606"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("93b909df-04d1-4864-814f-87f82b6d61b0"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b3c985e2-d502-4f3d-9937-84e33dfa1cf1"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ebf16014-ba1b-412f-b3dd-38fbc3deee82"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ecf8dfd5-bba0-4e9a-8f3b-efc3e646a3e5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f0b62fa5-2f72-47a1-bdc5-26b036563c75"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("fc789d30-88f4-4f47-995f-9d4901148d9e"));

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("29ab2ac4-1173-498a-b353-c9dc8ad5cfb4"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("2edd8a4c-2d71-4b51-9af9-37fd1716d6f6"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("3f0809b0-578c-4c46-a00f-8b3d1c978223"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("4836485e-858d-4e3a-ae02-092654c9ae42"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("49fa1d9b-509d-458b-9212-bb2079542575"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("6be090d1-6d91-4c2f-8d81-4907ddd30fe4"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("9101c095-7b30-402b-8cfd-3ad1f793135b"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("a7acfc23-f7e9-41b2-8093-ab34f60cc802"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("ae1ca1ce-4e82-4ed7-99be-e79b3ccdbf9e"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("bfc00ec8-844b-4324-864b-0b7496b51d09"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("cc55eb86-cdc6-48f6-b008-e771a5724bc3"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("d7a3df32-4443-4131-bea2-99b1811decf1"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("f3d5e666-adf3-4bba-9475-87782aad76c5"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("f82e6400-5d67-4bc5-bc1c-c4600b96ac12"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "EnumRole", "LastModified", "LastModifiedBy", "ModerationStatus", "RoleName" },
                values: new object[] { new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d3e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Giao hàng", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Giao hàng" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$KeNBiwIkyFNXZBCMj7ptCec/7HQO05fRTmbBVssqxdug6DDigy76G");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$OIOYdglFZV8SOEM7XgwsZu8dcLacSW3BgE2kPBh/ZxRpp5kag.RNe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("29ab2ac4-1173-498a-b353-c9dc8ad5cfb4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("2edd8a4c-2d71-4b51-9af9-37fd1716d6f6"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("3f0809b0-578c-4c46-a00f-8b3d1c978223"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("4836485e-858d-4e3a-ae02-092654c9ae42"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("49fa1d9b-509d-458b-9212-bb2079542575"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("6be090d1-6d91-4c2f-8d81-4907ddd30fe4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9101c095-7b30-402b-8cfd-3ad1f793135b"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a7acfc23-f7e9-41b2-8093-ab34f60cc802"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ae1ca1ce-4e82-4ed7-99be-e79b3ccdbf9e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("bfc00ec8-844b-4324-864b-0b7496b51d09"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("cc55eb86-cdc6-48f6-b008-e771a5724bc3"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d7a3df32-4443-4131-bea2-99b1811decf1"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f3d5e666-adf3-4bba-9475-87782aad76c5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f82e6400-5d67-4bc5-bc1c-c4600b96ac12"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d3e"));

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0c714975-9863-45f1-9916-fa1466a73586"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("19e3c551-1f82-4df8-a1af-ce1d591dffe5"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("232348f3-5b7d-46b2-9f58-9ac101e3208f"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("39277d63-2c5e-48c3-8f29-4c54a1ed4ef3"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("5d98ee2d-42a0-4413-be1f-d3d19b79582a"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("6e4a7329-c091-4c94-a92d-d3707bec8438"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("71766ff7-6242-4508-ac08-a683ec343c52"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("89a1745e-d614-4b22-a0c3-0f0521f70606"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("93b909df-04d1-4864-814f-87f82b6d61b0"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("b3c985e2-d502-4f3d-9937-84e33dfa1cf1"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("ebf16014-ba1b-412f-b3dd-38fbc3deee82"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("ecf8dfd5-bba0-4e9a-8f3b-efc3e646a3e5"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("f0b62fa5-2f72-47a1-bdc5-26b036563c75"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("fc789d30-88f4-4f47-995f-9d4901148d9e"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$KhhYB2IGeHRueA7mdrJcLOgoaxfPB5nCbPnLbQmzl5f6ABYgYheiW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$aJbhy20KrUIrh.FNP6yOaeF/bPFEzWgVp1frME0dDAB5m6QCpB8/.");
        }
    }
}
