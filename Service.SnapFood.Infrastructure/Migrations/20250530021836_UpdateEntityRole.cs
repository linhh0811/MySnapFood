using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("354ba95f-9276-4f81-bc22-cac7f336413e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("36f83afe-aad1-4984-9f73-2d23c0fd9370"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("43c028f2-ba31-4c44-9905-ad6d9d8e1330"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("856f5bc7-f3ed-495f-9c25-0157875b907b"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c53093e8-4a84-4578-b855-892d4bf250d3"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e332021d-1c42-4980-9014-4d36784e69a4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f131c66e-fc1f-4af7-acb0-d7a5969b0103"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f1d350f8-8f68-435e-868e-ef1ae2006aeb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("fc3b6149-3adc-445c-b095-fb05d6a97a49"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("113a873b-4fdc-429e-a370-13fd4d6e535a"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("19a8ca13-16a7-49ca-b9a4-6bd25cc24ac3"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("29c0e456-1c78-47c3-bea5-732417fb9bad"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("2bdc8d71-09d3-4cc0-aaf9-4ff37b1a5431"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("2dc8c59f-efc5-4d89-a954-44e08acaa437"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("59b60f91-2587-407e-937d-2c1eb2bf42b8"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("6991cc19-e378-4290-921e-c614256c5f12"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("6bcffcc1-803f-4bb5-968a-64680d90396a"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("808ccf6f-c571-4ec8-8885-dc5096ae4f64"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("8270e2a0-5c6b-4354-8c4e-8475e9d11b34"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("82a3e06c-6479-4d91-b861-bf985271c2f9"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("8961fe6f-9041-4d2b-add4-dd94b6f11ba1"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("9da1c677-e2d5-481c-8ae6-08d016d794e9"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("f3522bf0-205f-448c-8ad4-959390f0da99"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("f95a5298-a2b9-4fa3-8d3d-ad8ce3f5bb40"));

            migrationBuilder.AddColumn<int>(
                name: "EnumRole",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("28255972-bb77-4eda-8340-9311adc2bd21"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("293f500b-3c56-4be8-849b-9f54f432d683"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("77ebca3b-b688-454e-a69c-04b12b32ae42"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("93eeba95-f5f0-42e7-9533-6217f81d7fc6"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("969d770d-03b4-44b6-90df-9330e7de03ef"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("c4af1e5a-f161-4310-8d8a-f9334589da16"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("d010aaba-6c5f-459a-8c22-22482e3079d5"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("f2879af4-e972-4b44-9fa6-f04006c2944e"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("fd567d15-2706-4f6e-a25e-e94b4a4a37c8"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"),
                column: "EnumRole",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d2e"),
                column: "EnumRole",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"),
                column: "EnumRole",
                value: 0);

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("266ae2c9-dcea-4889-82d4-e6b07a90dd20"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("26ad9aa7-18ee-44a3-9a3a-0e63b93d46da"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("484a0175-9cc6-44ec-a3cc-919288027546"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("48d680fa-0365-4c95-9ec3-27692986fda6"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("6ace9914-8369-4ea9-aa81-871616435442"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("74c23dd9-0354-4dd7-a0d8-718536c687c0"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("7a63e5db-af54-405c-9d1e-0b776d1aa243"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("7f069625-39b1-45c0-9510-ddd0c7e89ed4"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("830997be-45c7-4ab2-890f-616dd4f92f49"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("8ad90b0c-4468-4ae3-ab83-6caba47ed3ad"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" },
                    { new Guid("9abfccd1-d8bd-49eb-a63d-315526e9baae"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("e1188002-f2b6-49d8-9312-95ba484c5926"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("41b94861-b196-436d-8a77-73e84d86c866"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("958378a8-d1f9-4cee-be4a-e86951b423ae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("d3b9da6e-9ce7-41b1-b5da-c9e2b4891a4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$upMd8kDDAQGpmNeLnoniFexQzE9G26EMbh4Htog7OAKEHT5luqGEe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$CICo0280/LBiDdSocvRYp.7vgSQTrgFar9mQ273F79TCmUmZxxjru");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("28255972-bb77-4eda-8340-9311adc2bd21"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("293f500b-3c56-4be8-849b-9f54f432d683"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("77ebca3b-b688-454e-a69c-04b12b32ae42"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("93eeba95-f5f0-42e7-9533-6217f81d7fc6"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("969d770d-03b4-44b6-90df-9330e7de03ef"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c4af1e5a-f161-4310-8d8a-f9334589da16"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d010aaba-6c5f-459a-8c22-22482e3079d5"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f2879af4-e972-4b44-9fa6-f04006c2944e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("fd567d15-2706-4f6e-a25e-e94b4a4a37c8"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("266ae2c9-dcea-4889-82d4-e6b07a90dd20"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("26ad9aa7-18ee-44a3-9a3a-0e63b93d46da"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("484a0175-9cc6-44ec-a3cc-919288027546"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("48d680fa-0365-4c95-9ec3-27692986fda6"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("6ace9914-8369-4ea9-aa81-871616435442"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("74c23dd9-0354-4dd7-a0d8-718536c687c0"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("7a63e5db-af54-405c-9d1e-0b776d1aa243"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("7f069625-39b1-45c0-9510-ddd0c7e89ed4"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("830997be-45c7-4ab2-890f-616dd4f92f49"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("8ad90b0c-4468-4ae3-ab83-6caba47ed3ad"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("9abfccd1-d8bd-49eb-a63d-315526e9baae"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("e1188002-f2b6-49d8-9312-95ba484c5926"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("41b94861-b196-436d-8a77-73e84d86c866"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("958378a8-d1f9-4cee-be4a-e86951b423ae"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("d3b9da6e-9ce7-41b1-b5da-c9e2b4891a4c"));

            migrationBuilder.DropColumn(
                name: "EnumRole",
                table: "Roles");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("354ba95f-9276-4f81-bc22-cac7f336413e"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("36f83afe-aad1-4984-9f73-2d23c0fd9370"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("43c028f2-ba31-4c44-9905-ad6d9d8e1330"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("856f5bc7-f3ed-495f-9c25-0157875b907b"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("c53093e8-4a84-4578-b855-892d4bf250d3"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("e332021d-1c42-4980-9014-4d36784e69a4"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("f131c66e-fc1f-4af7-acb0-d7a5969b0103"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("f1d350f8-8f68-435e-868e-ef1ae2006aeb"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("fc3b6149-3adc-445c-b095-fb05d6a97a49"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("113a873b-4fdc-429e-a370-13fd4d6e535a"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("19a8ca13-16a7-49ca-b9a4-6bd25cc24ac3"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("29c0e456-1c78-47c3-bea5-732417fb9bad"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("2bdc8d71-09d3-4cc0-aaf9-4ff37b1a5431"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("2dc8c59f-efc5-4d89-a954-44e08acaa437"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("59b60f91-2587-407e-937d-2c1eb2bf42b8"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("6991cc19-e378-4290-921e-c614256c5f12"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("6bcffcc1-803f-4bb5-968a-64680d90396a"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("808ccf6f-c571-4ec8-8885-dc5096ae4f64"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("8270e2a0-5c6b-4354-8c4e-8475e9d11b34"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("82a3e06c-6479-4d91-b861-bf985271c2f9"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("8961fe6f-9041-4d2b-add4-dd94b6f11ba1"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("9da1c677-e2d5-481c-8ae6-08d016d794e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("f3522bf0-205f-448c-8ad4-959390f0da99"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("f95a5298-a2b9-4fa3-8d3d-ad8ce3f5bb40"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$hfUavUdknuucYxRafesaoehNAZsDl825Ac/eYhsKn4dAA.fD./m8e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$AeE6SPxM54xLGVolLz4FOu85MVN5oIanvBf3DsdnMXji.u6fUHVMy");
        }
    }
}
