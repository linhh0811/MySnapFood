using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThemTrangThaiBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("18319314-cbca-443f-abd3-5df0cfc4090f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1bb917f2-e91f-499b-9c48-f9370d2d6d19"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("501f4887-8b6d-491e-8056-6d1548a77b44"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("54ce3ee6-c328-4139-98df-ffbe02c35572"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("67b8af49-3e1a-4304-80be-9e2ec19f8885"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a42035bf-464c-4faa-b950-80c715ae3aff"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("adff8686-f6ea-4cb7-ac87-6b7580510dbf"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ae6a499f-45d8-4631-b9f9-412910558d11"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b0fcae23-ca2f-4ddd-92a4-2bc8f6af66f2"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c275247c-4e1b-4765-aca3-e411be63dfd1"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d0e0373a-91c6-4779-a471-83f62a83c108"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ede7dafd-61cd-4b28-96ec-cc50f876b9a2"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f4c7ba7d-cf6e-41ee-9cff-2ca223e9c551"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("fb582eac-7622-4020-980f-0ce75b217012"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("025dbe2a-3fbd-42db-abe7-8cef8a063f89"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("06818560-707f-45d1-ace3-76e4ba96e690"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("09daaf01-c6dd-4f3a-8cfd-52a2fe00f34d"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("27d42985-4ef4-4d84-95ea-77d97868c9f6"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("30c8348a-f8ae-41f0-8e5e-03b397acb038"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("55397c1a-9788-4d48-8f8e-fec3abccf253"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("7c61e81b-9f6e-4177-b967-e6b4933b239f"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("7fe7539b-1ac7-4f36-a264-c5cb7d8f438e"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("8415147c-2938-4286-9584-6f0f61e9e508"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("9d6e90e3-c3a4-4c6a-99ce-869a91738682"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("ef9881a9-dc99-46b4-82dc-0b5f2ee4f719"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("f52d866a-ea9c-4926-a3bf-c16ee4b2148e"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("85fee37d-af14-4b41-9089-360b65309ef9"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("e9051a1e-d219-4926-abc4-0b5c08d799b3"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("ff432a74-aa90-4975-bcfc-6c560413049e"));

            migrationBuilder.AddColumn<int>(
                name: "PhuongThucDatHang",
                table: "Bill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceivingType",
                table: "Bill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("1ba795d5-0cdc-491f-b4de-f2c6256fd3c5"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("21fa1e08-903b-4309-a0c8-8b2dc58e169f"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("24846460-10c0-4e5a-a5c9-5d38ff1dd50f"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("28ebc777-db14-4eb4-b99f-017f33405873"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("37f41d24-90d1-4b45-af53-476e11a4b4f4"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("425156c8-da03-4157-bd27-6d0ce3e32ac9"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("5167e5f1-230c-4238-ac46-a7cae52ebd58"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("7b90d969-2acd-4a69-b7ca-cd7124a2fc44"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("82f7b5d2-4b01-4b63-82ea-f2fa9228ffff"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("9aa4ba72-a080-4afa-9c26-3b4e93181ab2"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("9f96f3d1-0935-4136-a0fd-28d816235b49"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("a0b36e60-b29e-471f-84bb-6bbd8e298ee7"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("b5db7c25-3ca9-464b-b165-42a2469a8314"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("ffa5a6c3-154b-4ed0-bbf5-40daec253fab"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("087d64da-1e16-49f6-b44a-fc784c519b56"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("0f97fdd0-6014-4260-a66f-2c20037a1212"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("20bf12f9-f3e1-4589-9f8b-2c58b42f7624"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("2af06c93-5dc3-48cf-9434-a61d02d23149"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("3b46f509-4745-4885-80d7-c0a5a2a0ce3f"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("473d7f63-8220-4625-b0d7-51d050a41c3a"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("59c04244-a75b-48e8-91db-d5ddde75b9b7"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" },
                    { new Guid("811cd805-1c2b-4748-a70a-97d8a84bd4fe"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("813721d9-f8ea-4e39-a809-48838d74f371"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("a9a9c55a-cfd0-4c26-bf12-93cd4c2afb70"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("ed7a2d1a-fb3b-43fb-a822-0bd6e0264258"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("fdce690a-768d-470d-9067-524724249d2a"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6e51bc78-d8d5-465e-ab00-aa78077e0927"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("a4ff6af0-599d-468a-9796-aacf4868df40"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("aaccb080-8b14-4c88-8b1e-291aae3e4b79"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$6xSjCRq/iyHolRaQBv.p1erZ2iK02wOdC/UPBVJVVz1bEcQMn4nYS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$e0Y97ilikelkqo9urtQyQOKblKgwH8M.H4mAPBE8QphIPcCvOmT1W");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1ba795d5-0cdc-491f-b4de-f2c6256fd3c5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("21fa1e08-903b-4309-a0c8-8b2dc58e169f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("24846460-10c0-4e5a-a5c9-5d38ff1dd50f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("28ebc777-db14-4eb4-b99f-017f33405873"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("37f41d24-90d1-4b45-af53-476e11a4b4f4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("425156c8-da03-4157-bd27-6d0ce3e32ac9"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("5167e5f1-230c-4238-ac46-a7cae52ebd58"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7b90d969-2acd-4a69-b7ca-cd7124a2fc44"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("82f7b5d2-4b01-4b63-82ea-f2fa9228ffff"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9aa4ba72-a080-4afa-9c26-3b4e93181ab2"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9f96f3d1-0935-4136-a0fd-28d816235b49"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a0b36e60-b29e-471f-84bb-6bbd8e298ee7"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b5db7c25-3ca9-464b-b165-42a2469a8314"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ffa5a6c3-154b-4ed0-bbf5-40daec253fab"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("087d64da-1e16-49f6-b44a-fc784c519b56"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("0f97fdd0-6014-4260-a66f-2c20037a1212"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("20bf12f9-f3e1-4589-9f8b-2c58b42f7624"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("2af06c93-5dc3-48cf-9434-a61d02d23149"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("3b46f509-4745-4885-80d7-c0a5a2a0ce3f"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("473d7f63-8220-4625-b0d7-51d050a41c3a"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("59c04244-a75b-48e8-91db-d5ddde75b9b7"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("811cd805-1c2b-4748-a70a-97d8a84bd4fe"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("813721d9-f8ea-4e39-a809-48838d74f371"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("a9a9c55a-cfd0-4c26-bf12-93cd4c2afb70"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("ed7a2d1a-fb3b-43fb-a822-0bd6e0264258"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("fdce690a-768d-470d-9067-524724249d2a"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("6e51bc78-d8d5-465e-ab00-aa78077e0927"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("a4ff6af0-599d-468a-9796-aacf4868df40"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("aaccb080-8b14-4c88-8b1e-291aae3e4b79"));

            migrationBuilder.DropColumn(
                name: "PhuongThucDatHang",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "ReceivingType",
                table: "Bill");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("18319314-cbca-443f-abd3-5df0cfc4090f"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("1bb917f2-e91f-499b-9c48-f9370d2d6d19"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("501f4887-8b6d-491e-8056-6d1548a77b44"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("54ce3ee6-c328-4139-98df-ffbe02c35572"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("67b8af49-3e1a-4304-80be-9e2ec19f8885"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("a42035bf-464c-4faa-b950-80c715ae3aff"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("adff8686-f6ea-4cb7-ac87-6b7580510dbf"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("ae6a499f-45d8-4631-b9f9-412910558d11"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("b0fcae23-ca2f-4ddd-92a4-2bc8f6af66f2"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("c275247c-4e1b-4765-aca3-e411be63dfd1"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("d0e0373a-91c6-4779-a471-83f62a83c108"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("ede7dafd-61cd-4b28-96ec-cc50f876b9a2"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("f4c7ba7d-cf6e-41ee-9cff-2ca223e9c551"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("fb582eac-7622-4020-980f-0ce75b217012"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("025dbe2a-3fbd-42db-abe7-8cef8a063f89"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("06818560-707f-45d1-ace3-76e4ba96e690"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("09daaf01-c6dd-4f3a-8cfd-52a2fe00f34d"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("27d42985-4ef4-4d84-95ea-77d97868c9f6"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("30c8348a-f8ae-41f0-8e5e-03b397acb038"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("55397c1a-9788-4d48-8f8e-fec3abccf253"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("7c61e81b-9f6e-4177-b967-e6b4933b239f"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("7fe7539b-1ac7-4f36-a264-c5cb7d8f438e"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" },
                    { new Guid("8415147c-2938-4286-9584-6f0f61e9e508"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("9d6e90e3-c3a4-4c6a-99ce-869a91738682"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("ef9881a9-dc99-46b4-82dc-0b5f2ee4f719"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("f52d866a-ea9c-4926-a3bf-c16ee4b2148e"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("85fee37d-af14-4b41-9089-360b65309ef9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("e9051a1e-d219-4926-abc4-0b5c08d799b3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("ff432a74-aa90-4975-bcfc-6c560413049e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$VZYL1HJvxx.y6WSV.Z.nBeUaavAkimsi6kepzW.44fJRu3ffkGFK2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$bO/ZFcnecuWnIVQUswuvWuJ.T7u9I7hBRAprNKxMCOoEpbYtkw4j.");
        }
    }
}
