using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePromotionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("0f09a96c-74eb-4f5a-9530-d12aa0af6ef7"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("290e5eb1-cdd2-4a2e-967f-722c99f0b020"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("33c78ced-d074-4053-ba00-afd22262a609"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("73c18f34-7195-49aa-8bb7-dc041d2176bb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7afe6e19-d1f3-4799-b7f0-2a53f65dc27d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("84c94fa6-aa1c-4618-b8b5-40c2c25fe034"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("88918322-08e9-45ab-b3de-e75b88db7de7"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9b5b0624-958c-41cb-87b8-1fd60cacff01"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a5f42d3e-73eb-46c9-97d3-c66a2d282654"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b0e5adac-af60-4e80-a81c-5b339f7dad5f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("baacb0ed-c37f-421f-92f2-08aac753e3cc"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("bb0f3dc2-8c7c-479a-93a2-d2859233c651"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d6a6d242-8f2c-494d-a52e-c7b0de11f441"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d91dbc3a-274c-4617-a0b5-f30c2b9a8547"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PromotionItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("32b5061b-c696-486a-9463-c005322db367"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("4af3adc5-1c45-4e29-bc3c-c70fc3707552"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("4b39db6d-0088-4ba1-ba63-f0c3b0b4c219"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("676a27a6-1f26-4d07-ac17-f9ee8a1283cb"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("91e0b263-7a2d-426d-a910-6d3eb485f586"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("9d92ff7e-5c9a-4e35-8555-fd61f3ba8035"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("ac8cc08a-d01a-4428-ae02-37f68ed3b811"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("b3888583-5f2b-40ac-98be-ca60abe3678c"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("dd46e41d-5285-46e8-acff-0c7b616967bd"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("dda135a6-c476-4e68-82f6-50862fd3f8f8"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("df1a9356-e48d-4791-be2a-127901e0a327"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("e1a1fd87-b36b-4927-a36f-a5cb39cbcefd"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("e40c7862-9a0f-49cb-945f-22cec21bd0d4"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("eea348a2-4732-4af8-81a7-c073106ea003"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$ha7l2hIpjpBO1xvbWRu7q.tld2oFuBVConKsP176YP027nQF1N5JC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$PHqSCSQ1wNySTyO9v8pdgeZ4kpANzXgIa9tIv8yHd/y3s7kAbJVga");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("32b5061b-c696-486a-9463-c005322db367"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("4af3adc5-1c45-4e29-bc3c-c70fc3707552"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("4b39db6d-0088-4ba1-ba63-f0c3b0b4c219"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("676a27a6-1f26-4d07-ac17-f9ee8a1283cb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("91e0b263-7a2d-426d-a910-6d3eb485f586"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9d92ff7e-5c9a-4e35-8555-fd61f3ba8035"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ac8cc08a-d01a-4428-ae02-37f68ed3b811"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("b3888583-5f2b-40ac-98be-ca60abe3678c"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("dd46e41d-5285-46e8-acff-0c7b616967bd"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("dda135a6-c476-4e68-82f6-50862fd3f8f8"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("df1a9356-e48d-4791-be2a-127901e0a327"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e1a1fd87-b36b-4927-a36f-a5cb39cbcefd"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e40c7862-9a0f-49cb-945f-22cec21bd0d4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("eea348a2-4732-4af8-81a7-c073106ea003"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PromotionItem");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0f09a96c-74eb-4f5a-9530-d12aa0af6ef7"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("290e5eb1-cdd2-4a2e-967f-722c99f0b020"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("33c78ced-d074-4053-ba00-afd22262a609"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("73c18f34-7195-49aa-8bb7-dc041d2176bb"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("7afe6e19-d1f3-4799-b7f0-2a53f65dc27d"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("84c94fa6-aa1c-4618-b8b5-40c2c25fe034"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("88918322-08e9-45ab-b3de-e75b88db7de7"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("9b5b0624-958c-41cb-87b8-1fd60cacff01"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("a5f42d3e-73eb-46c9-97d3-c66a2d282654"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("b0e5adac-af60-4e80-a81c-5b339f7dad5f"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("baacb0ed-c37f-421f-92f2-08aac753e3cc"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("bb0f3dc2-8c7c-479a-93a2-d2859233c651"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("d6a6d242-8f2c-494d-a52e-c7b0de11f441"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("d91dbc3a-274c-4617-a0b5-f30c2b9a8547"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$RjPRKTmEb0xhGCZE/uuEa.SEIn8jrgxP.fVr5gca15ekZB7qvdFj6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$YZ/0CRbf9I8DA6ejtGK39urzfLCGsifoAjGV/cANrSy8E4dzIPQOu");
        }
    }
}
