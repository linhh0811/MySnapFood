using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStore2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("11d426cd-7b4d-4aa9-a0a0-59898bb8b006"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1849a1b6-b3a8-45e0-8c46-84fa5731c912"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("2a82cfed-27fa-4f06-955f-aaa4108c3a51"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("2cc956eb-897e-4e82-942b-e30be59a4aff"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("41bb1067-e8ce-42bf-9be7-e76f6053aed0"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("70dd1ae5-0045-446a-abb1-3c85a22935f9"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("84bf6c54-80ae-4167-b8b1-f38fc06b29df"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("8d0d8525-9b71-49a4-9fd8-401b36a15960"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9d9ca585-e7e9-487e-af39-c1e1e9e7fc2e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("aa782b69-3621-4084-a570-2987ebf5fb0d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b3c95bf0-5ee0-4d0c-a0e2-2a1105ee7802"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("be2dc557-f942-4bea-8c6f-feccf66b09fb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("dbd375f9-cf9b-4464-97a9-00ce2798a0fd"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("f9740864-d7ae-4dcf-adb8-be46dbea339f"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("0baf5b25-8f5d-4d7a-9457-2feb67a736e2"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("238df55b-e8a5-4a72-9c59-7a948c0a5867"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("2a6fb2ae-837a-4d63-be7d-e01ac3368be1"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("2ac3aac4-2974-4d0b-89d1-d59207e685f9"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("411caf61-8391-4791-9d49-8ab051990610"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("87d99d90-f2c3-4ebc-a035-aee80d0f2e19"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("8883f767-08f1-4965-8ae6-b32bba393901"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("994418f3-f6c2-4674-935c-8562700ef38b"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("9bd02ff9-b2e0-40da-94a7-9a49759f1027"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("c2378c6d-83f6-4fdd-9f6d-c972a1cab846"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("dc413cc2-1377-4258-b38c-c269993c12cf"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("f99a7e30-a847-4cef-bd0e-1168f018ff27"));

            migrationBuilder.DeleteData(
                table: "ThongTinGiaoHangs",
                keyColumn: "Id",
                keyValue: new Guid("7653ea91-fa4b-473e-b2d3-f91b9b84a5b9"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("5edbbcf7-261b-4853-b411-886e71e5747d"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("62b29a5b-2cdf-4e8d-bf6f-c5e281ea8d02"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("71f958b3-6125-4e77-aeca-90ffe0ed1ffc"));

            migrationBuilder.AddColumn<string>(
                name: "NumberPhone",
                table: "Store",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0a6574f0-e51a-4a09-ad67-6e3a5e7f8bfc"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("1af7f226-e2ed-4721-a86e-81ebc4f2bbe2"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("1b4c6684-23c7-43f7-94d3-83b52fab1ef8"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("31548759-4cdd-4e43-a45e-b488c98819d6"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("47f092ba-4bc5-4663-a04d-bd0479766fd0"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("51e73ddc-dac3-4275-aa2c-032c337a34a8"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("57654d0a-d08d-4121-ba58-d08924f873bb"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("76627625-87a3-4edb-89f2-4f1e4b92fff7"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("91c6856e-f4ac-44f0-9533-bd028518d135"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("926286e4-ca4a-4500-b697-bd7b8eeaa034"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("9a6e82ff-b271-4b93-b7d6-08b67a18b792"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("cd725a86-697f-4226-b166-b11f697b8a6e"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("ce7e19a1-5f7c-46c3-83af-df8e943dabf9"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("d6cd75b9-7f27-4a9f-a68c-7c3157edc3b7"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("0352dcb4-0657-49c8-80f1-012b8fc0d83e"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" },
                    { new Guid("11447fa0-f061-49e0-ae85-d3d2b6440f89"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("13c2ebe7-249f-4120-a261-3d90bb87af62"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("1a74b0c5-d4df-4b89-950f-5d1d49ac831d"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("1ce5ac63-03bc-4876-a404-4b129abb7a6a"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("2edaf3ac-2702-4c29-97d2-950418a1c068"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("457a6ee4-8d5f-4a3e-a137-ab25e67215b8"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("710e9e38-ea48-4db9-8757-5b5dce6d0076"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("7985a18d-0caa-4d46-8afb-6b58b84a6ed3"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("bc661706-85c1-408d-8b6f-37e042e0e40f"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("ccc8eebe-cafb-4710-99ce-254109278685"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" },
                    { new Guid("d4b213a8-418d-46da-8f0c-82b99680a529"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" }
                });

            migrationBuilder.UpdateData(
                table: "Store",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                column: "NumberPhone",
                value: "055931234");

            migrationBuilder.InsertData(
                table: "ThongTinGiaoHangs",
                columns: new[] { "Id", "BanKinhGiaoHang", "Created", "DonHangToiThieu", "PhiGiaoHang" },
                values: new object[] { new Guid("75d50183-ce7f-463b-ab95-f4f92c5fce39"), 15.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000m, 5000m });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("30d63c9a-f762-40e3-b286-6eed1647b105"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("4be0393f-8227-4c14-8dfb-f73c4c84db4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("9a69944f-4bc1-4ee1-859d-46eebc806eed"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$kmmIX1JBxTCY6Hx3nBBMVuCeilPrmdhiuBqMm56g9SjcsvIfWfVMW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$ZvvGBdBWIB61vgCHVFM02eZ8IcfVCUjlz3wAoul6A7Z7GFu03KQeG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0a6574f0-e51a-4a09-ad67-6e3a5e7f8bfc"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1af7f226-e2ed-4721-a86e-81ebc4f2bbe2"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("1b4c6684-23c7-43f7-94d3-83b52fab1ef8"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("31548759-4cdd-4e43-a45e-b488c98819d6"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("47f092ba-4bc5-4663-a04d-bd0479766fd0"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("51e73ddc-dac3-4275-aa2c-032c337a34a8"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("57654d0a-d08d-4121-ba58-d08924f873bb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("76627625-87a3-4edb-89f2-4f1e4b92fff7"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("91c6856e-f4ac-44f0-9533-bd028518d135"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("926286e4-ca4a-4500-b697-bd7b8eeaa034"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9a6e82ff-b271-4b93-b7d6-08b67a18b792"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("cd725a86-697f-4226-b166-b11f697b8a6e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ce7e19a1-5f7c-46c3-83af-df8e943dabf9"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d6cd75b9-7f27-4a9f-a68c-7c3157edc3b7"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("0352dcb4-0657-49c8-80f1-012b8fc0d83e"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("11447fa0-f061-49e0-ae85-d3d2b6440f89"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("13c2ebe7-249f-4120-a261-3d90bb87af62"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("1a74b0c5-d4df-4b89-950f-5d1d49ac831d"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("1ce5ac63-03bc-4876-a404-4b129abb7a6a"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("2edaf3ac-2702-4c29-97d2-950418a1c068"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("457a6ee4-8d5f-4a3e-a137-ab25e67215b8"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("710e9e38-ea48-4db9-8757-5b5dce6d0076"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("7985a18d-0caa-4d46-8afb-6b58b84a6ed3"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("bc661706-85c1-408d-8b6f-37e042e0e40f"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("ccc8eebe-cafb-4710-99ce-254109278685"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: new Guid("d4b213a8-418d-46da-8f0c-82b99680a529"));

            migrationBuilder.DeleteData(
                table: "ThongTinGiaoHangs",
                keyColumn: "Id",
                keyValue: new Guid("75d50183-ce7f-463b-ab95-f4f92c5fce39"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("30d63c9a-f762-40e3-b286-6eed1647b105"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("4be0393f-8227-4c14-8dfb-f73c4c84db4b"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("9a69944f-4bc1-4ee1-859d-46eebc806eed"));

            migrationBuilder.DropColumn(
                name: "NumberPhone",
                table: "Store");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("11d426cd-7b4d-4aa9-a0a0-59898bb8b006"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("1849a1b6-b3a8-45e0-8c46-84fa5731c912"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("2a82cfed-27fa-4f06-955f-aaa4108c3a51"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("2cc956eb-897e-4e82-942b-e30be59a4aff"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("41bb1067-e8ce-42bf-9be7-e76f6053aed0"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("70dd1ae5-0045-446a-abb1-3c85a22935f9"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("84bf6c54-80ae-4167-b8b1-f38fc06b29df"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("8d0d8525-9b71-49a4-9fd8-401b36a15960"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("9d9ca585-e7e9-487e-af39-c1e1e9e7fc2e"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("aa782b69-3621-4084-a570-2987ebf5fb0d"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("b3c95bf0-5ee0-4d0c-a0e2-2a1105ee7802"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("be2dc557-f942-4bea-8c6f-feccf66b09fb"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("dbd375f9-cf9b-4464-97a9-00ce2798a0fd"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("f9740864-d7ae-4dcf-adb8-be46dbea339f"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("0baf5b25-8f5d-4d7a-9457-2feb67a736e2"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "XL" },
                    { new Guid("238df55b-e8a5-4a72-9c59-7a948c0a5867"), 4000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "L" },
                    { new Guid("2a6fb2ae-837a-4d63-be7d-e01ac3368be1"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Mega" },
                    { new Guid("2ac3aac4-2974-4d0b-89d1-d59207e685f9"), 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Lớn" },
                    { new Guid("411caf61-8391-4791-9d49-8ab051990610"), 10000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Big" },
                    { new Guid("87d99d90-f2c3-4ebc-a035-aee80d0f2e19"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Nhỏ" },
                    { new Guid("8883f767-08f1-4965-8ae6-b32bba393901"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần thường" },
                    { new Guid("994418f3-f6c2-4674-935c-8562700ef38b"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), "M" },
                    { new Guid("9bd02ff9-b2e0-40da-94a7-9a49759f1027"), 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), "Vừa" },
                    { new Guid("c2378c6d-83f6-4fdd-9f6d-c972a1cab846"), 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần lớn" },
                    { new Guid("dc413cc2-1377-4258-b38c-c269993c12cf"), 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), "Phần đại" },
                    { new Guid("f99a7e30-a847-4cef-bd0e-1168f018ff27"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), "Tiêu chuẩn" }
                });

            migrationBuilder.InsertData(
                table: "ThongTinGiaoHangs",
                columns: new[] { "Id", "BanKinhGiaoHang", "Created", "DonHangToiThieu", "PhiGiaoHang" },
                values: new object[] { new Guid("7653ea91-fa4b-473e-b2d3-f91b9b84a5b9"), 15.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50000m, 5000m });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Created", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("5edbbcf7-261b-4853-b411-886e71e5747d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") },
                    { new Guid("62b29a5b-2cdf-4e8d-bf6f-c5e281ea8d02"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e") },
                    { new Guid("71f958b3-6125-4e77-aeca-90ffe0ed1ffc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$L4pJ9.dkHhj.52b0IPyMROkmXurDtvLD/nO0UkOR1B2E.Drh8XEoa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$3ApNLIRDvnfVs4eqX1kWpOyIsYLLQ2pv4TCFSg5lAmg/m4En.vHkW");
        }
    }
}
