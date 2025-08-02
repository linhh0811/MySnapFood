using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("051b4b1d-2b58-4383-9abc-376358dba99d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("23724504-3448-4b0e-b41b-9da274077694"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("42850115-44d1-41c4-a40e-16056095c109"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("6fd666fc-81d0-4cf2-a3c9-017e2d2c898f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("728bff96-c210-420e-a106-863ba82dbdc3"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("762b167e-42b6-4813-8353-09cd7a11f4c4"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7c74dab8-c8fe-4212-901e-660be652a9de"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("908fb4f3-34b9-427e-a71e-839560246342"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("a12d3405-102c-42f2-93d4-ee4b676cf761"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c13f8b78-0f9d-45e2-83d8-d104d999070a"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d61e49aa-db7a-420f-8fe3-455a43bea6a2"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d9dfe69c-b627-4831-8d8a-ea47b27b0646"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("dfc72c86-61e4-4fac-a9d3-23a2ca06a004"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e10b06a9-f22b-4c9d-ad26-758cea3693e8"));

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "BillPayments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BillPayments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("03979b3d-a936-4ed1-80c8-746d7b020399"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("121339d0-92b9-436f-8f03-ece743d7cb49"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("2ba0599e-0f3a-4c39-aabe-688bfa1befcb"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("49a657be-00cc-4932-a6b3-8f144b93e2ff"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("5c75dd24-2ea9-4b33-b40a-0c60934b5401"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("66bdb011-b514-4a56-9738-e5bad99f0079"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("6a01af9c-5c4c-45ef-a05e-139dd125a20a"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("7d0a6dc2-40ab-437c-a60d-28f856b79818"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("9ff76558-aabc-47ae-b5cf-ac143eebad84"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("c6c5180d-0876-4e39-8075-2f8c87d9c1b6"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("cd206b89-63cd-4876-82d9-4ce4cb9b455e"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("d8e4e218-c54d-47af-8173-67e9de5947a0"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("e7d539a4-2e6a-43d2-a326-dd376b5a9719"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("ee7341b3-3968-4e1a-89b6-322f5f460bba"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$MRkZwMxeAke2SFXnKNbzT.966a8I48hkoZvafnnECClpNJE/KTrq.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$fG3J2H5XtO/KDT7.axzVxe8VbW2qKRsYWqW46YKId8VC355ypHtGa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("03979b3d-a936-4ed1-80c8-746d7b020399"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("121339d0-92b9-436f-8f03-ece743d7cb49"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("2ba0599e-0f3a-4c39-aabe-688bfa1befcb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("49a657be-00cc-4932-a6b3-8f144b93e2ff"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("5c75dd24-2ea9-4b33-b40a-0c60934b5401"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("66bdb011-b514-4a56-9738-e5bad99f0079"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("6a01af9c-5c4c-45ef-a05e-139dd125a20a"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("7d0a6dc2-40ab-437c-a60d-28f856b79818"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("9ff76558-aabc-47ae-b5cf-ac143eebad84"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("c6c5180d-0876-4e39-8075-2f8c87d9c1b6"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("cd206b89-63cd-4876-82d9-4ce4cb9b455e"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("d8e4e218-c54d-47af-8173-67e9de5947a0"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("e7d539a4-2e6a-43d2-a326-dd376b5a9719"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "Id",
                keyValue: new Guid("ee7341b3-3968-4e1a-89b6-322f5f460bba"));

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "BillPayments",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "BillPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "Id", "ComboId", "Created", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("051b4b1d-2b58-4383-9abc-376358dba99d"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1 },
                    { new Guid("23724504-3448-4b0e-b41b-9da274077694"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("42850115-44d1-41c4-a40e-16056095c109"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), 1 },
                    { new Guid("6fd666fc-81d0-4cf2-a3c9-017e2d2c898f"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("728bff96-c210-420e-a106-863ba82dbdc3"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("762b167e-42b6-4813-8353-09cd7a11f4c4"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("7c74dab8-c8fe-4212-901e-660be652a9de"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1 },
                    { new Guid("908fb4f3-34b9-427e-a71e-839560246342"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("a12d3405-102c-42f2-93d4-ee4b676cf761"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 },
                    { new Guid("c13f8b78-0f9d-45e2-83d8-d104d999070a"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("d61e49aa-db7a-420f-8fe3-455a43bea6a2"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1 },
                    { new Guid("d9dfe69c-b627-4831-8d8a-ea47b27b0646"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1 },
                    { new Guid("dfc72c86-61e4-4fac-a9d3-23a2ca06a004"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), 1 },
                    { new Guid("e10b06a9-f22b-4c9d-ad26-758cea3693e8"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                column: "Password",
                value: "$2a$11$ExmUTMkEqABDy8f.tk0blexvsRifxCV6mBKXmqHwW.HKashM3XSAy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                column: "Password",
                value: "$2a$11$b.UjZVyqmGQDXzOb9uy/nOISNkeIwzDWtVnilghZj/R3mASLWxCV2");
        }
    }
}
