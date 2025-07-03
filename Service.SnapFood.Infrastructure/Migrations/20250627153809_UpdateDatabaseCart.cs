using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0b40a8a7-6dae-4861-915a-fde5c48af289"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0efeae4b-1c9b-4c70-b054-5a035ad45490"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("33fb9a08-ca22-4500-bd9b-4f99e2e0a5a7"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("3a9c68bd-6950-48f7-b654-94743bdfbe53"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("40d2265b-50ce-4218-bfb8-1e09955776f4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("51f36c21-d050-49dd-a2bf-2d86865166e8"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("809d12ed-47e2-4b70-b70f-8e1a69514330"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("846f284a-222d-49ce-93a8-590de6e66f23"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("8a46c5ac-d482-465e-ac9a-66624fbbc083"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("934450ec-5870-4745-b780-ac97c78787ed"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a57d1ca2-0f2f-432b-84c7-3ac711ff0c19"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d9e68972-c221-4234-a9bf-196694e81694"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("dd6d2636-7259-427e-be62-90b90827ff35"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f1466c23-b127-4414-a9f1-50d5f0de3efc"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("10ee6e4d-14aa-4c14-a7ff-cb3b089df433"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("40016f2e-3047-444e-849b-90053ce80bc8"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("496b4faf-c86c-4b51-b279-e67c569c7cf5"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("62b2aaa3-f890-40a0-bc82-37ccb01929c3"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("62d071cb-e604-4bb6-879a-0da77208ab77"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("65191eb2-27db-4567-b190-2dfd5edf5e25"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("93249e0e-b6fb-4755-9499-07ee376d5a3c"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("a58662e9-2c43-4bd9-9fca-a75a9032acd9"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("bbf88f8f-9669-40fb-93bf-e0698493692d"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("c16132b6-3250-4715-9e8a-4bfa428c0a70"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("cb1c07ed-0c0d-4b41-a31b-f0f8c0f33f20"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("f71cc3fb-9adb-4c30-93a5-8e28932b9d45"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("0c67f37b-ddf9-49f9-b76d-b6c0e7a9491b"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("7aad7d51-558e-4e51-8c38-0668d3726f9c"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("e663245f-4362-47c6-943e-b5d831c8f771"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SizeId",
                table: "ComboProductItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SizeId",
                table: "CartComboItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("001432f9-bcf6-4658-a18d-7d2355ef8f4f"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("06a97eaa-4200-4fb2-8064-26c9a166cbee"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("0b0f5af6-14d0-49ba-83fa-9da4423c38b8"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("100de5e4-4cc3-4f0c-a822-a663a75306f2"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("11badfdf-fdfe-4712-b9cb-b1ec84406c65"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("2bca8480-d20b-4831-baa0-2df3157c4a50"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("35082191-73b8-4ec4-ba48-7117fb2c737c"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("6cee3731-2e4b-4d35-b445-8d243346e32c"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("8b7bdf47-431a-46e2-95f3-55660c18d70c"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("ab40fd61-e114-4d68-95a3-bd50307bedea"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("ae5e2ddd-70c1-4d9a-8de3-8b4865fd3be9"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("b20c1277-65d4-4fdc-8ef3-1f514564825e"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("dcc9e791-5682-4b26-bdeb-ee426e2211b9"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("e5c27e5f-3086-418a-a565-17da34640484"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("0cae455e-e72a-45e8-b07e-15b4d9869fcd"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("14a181c3-1fa0-4c8b-9f3a-f0ffa0f53daa"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("1e4911cf-90aa-4b66-b932-d9e1a0eaf182"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("55a9b803-02e3-49be-9ee0-001c65915dbb"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("59e38999-e70a-4ac5-a678-27b4e58f2ee3"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("ab720625-aa55-47d3-8277-ecd80781adb5"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("acd2f684-9d00-4ccd-b2b8-9c29f56874b0"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("bdbc68c5-bae4-4ac7-8e39-93320b86a0f8"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("d3214a4d-6bd9-45a5-b134-025dd83d6e77"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("f1dc439f-4733-450c-ba01-9f30840b5c3c"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("f4e7fb01-c202-41c3-90c5-176f80819b27"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" },
                    { new Guid("fe970ae8-5051-4194-b5fd-a1ef9a23af32"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("170a1ed0-34d8-481c-95df-93e8fa527ed7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("c73b93ad-1b6d-4100-aed4-88b9fbd3f81a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("cfb94409-c54c-4232-86a0-e2998bad35d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$M9IzLduI.D8jPmQ6L3cdwuegxRkc6hd0dDI.AdB1ZWasx1IKmBZcm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$ziVaqBcBpeJG/N7dU3zixuTIXhc9m4PqJafpGsh.MdVrusMKOaZg6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("001432f9-bcf6-4658-a18d-7d2355ef8f4f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("06a97eaa-4200-4fb2-8064-26c9a166cbee"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0b0f5af6-14d0-49ba-83fa-9da4423c38b8"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("100de5e4-4cc3-4f0c-a822-a663a75306f2"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("11badfdf-fdfe-4712-b9cb-b1ec84406c65"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("2bca8480-d20b-4831-baa0-2df3157c4a50"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("35082191-73b8-4ec4-ba48-7117fb2c737c"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("6cee3731-2e4b-4d35-b445-8d243346e32c"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("8b7bdf47-431a-46e2-95f3-55660c18d70c"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ab40fd61-e114-4d68-95a3-bd50307bedea"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ae5e2ddd-70c1-4d9a-8de3-8b4865fd3be9"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b20c1277-65d4-4fdc-8ef3-1f514564825e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("dcc9e791-5682-4b26-bdeb-ee426e2211b9"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e5c27e5f-3086-418a-a565-17da34640484"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("0cae455e-e72a-45e8-b07e-15b4d9869fcd"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("14a181c3-1fa0-4c8b-9f3a-f0ffa0f53daa"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("1e4911cf-90aa-4b66-b932-d9e1a0eaf182"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("55a9b803-02e3-49be-9ee0-001c65915dbb"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("59e38999-e70a-4ac5-a678-27b4e58f2ee3"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("ab720625-aa55-47d3-8277-ecd80781adb5"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("acd2f684-9d00-4ccd-b2b8-9c29f56874b0"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("bdbc68c5-bae4-4ac7-8e39-93320b86a0f8"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("d3214a4d-6bd9-45a5-b134-025dd83d6e77"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("f1dc439f-4733-450c-ba01-9f30840b5c3c"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("f4e7fb01-c202-41c3-90c5-176f80819b27"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("fe970ae8-5051-4194-b5fd-a1ef9a23af32"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("170a1ed0-34d8-481c-95df-93e8fa527ed7"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("c73b93ad-1b6d-4100-aed4-88b9fbd3f81a"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("cfb94409-c54c-4232-86a0-e2998bad35d6"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SizeId",
                table: "ComboProductItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SizeId",
                table: "CartComboItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0b40a8a7-6dae-4861-915a-fde5c48af289"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("0efeae4b-1c9b-4c70-b054-5a035ad45490"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("33fb9a08-ca22-4500-bd9b-4f99e2e0a5a7"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("3a9c68bd-6950-48f7-b654-94743bdfbe53"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("40d2265b-50ce-4218-bfb8-1e09955776f4"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("51f36c21-d050-49dd-a2bf-2d86865166e8"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("809d12ed-47e2-4b70-b70f-8e1a69514330"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("846f284a-222d-49ce-93a8-590de6e66f23"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("8a46c5ac-d482-465e-ac9a-66624fbbc083"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("934450ec-5870-4745-b780-ac97c78787ed"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("a57d1ca2-0f2f-432b-84c7-3ac711ff0c19"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("d9e68972-c221-4234-a9bf-196694e81694"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("dd6d2636-7259-427e-be62-90b90827ff35"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("f1466c23-b127-4414-a9f1-50d5f0de3efc"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("10ee6e4d-14aa-4c14-a7ff-cb3b089df433"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("40016f2e-3047-444e-849b-90053ce80bc8"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("496b4faf-c86c-4b51-b279-e67c569c7cf5"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("62b2aaa3-f890-40a0-bc82-37ccb01929c3"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("62d071cb-e604-4bb6-879a-0da77208ab77"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("65191eb2-27db-4567-b190-2dfd5edf5e25"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("93249e0e-b6fb-4755-9499-07ee376d5a3c"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("a58662e9-2c43-4bd9-9fca-a75a9032acd9"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("bbf88f8f-9669-40fb-93bf-e0698493692d"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("c16132b6-3250-4715-9e8a-4bfa428c0a70"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("cb1c07ed-0c0d-4b41-a31b-f0f8c0f33f20"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("f71cc3fb-9adb-4c30-93a5-8e28932b9d45"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0c67f37b-ddf9-49f9-b76d-b6c0e7a9491b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("7aad7d51-558e-4e51-8c38-0668d3726f9c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("e663245f-4362-47c6-943e-b5d831c8f771"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$NWeiGWWjIt7a./ClpUS0uedN1BcvuYM/Q1LTQzNiIQa797yn7FBNm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$lHRdO2M.kfKQH38uWkUkS./ZvwBFZ30bGZiIqHUoY/Tn8GvCGb6HK");
        }
    }
}
