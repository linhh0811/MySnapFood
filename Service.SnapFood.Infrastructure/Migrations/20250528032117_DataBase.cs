using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.SnapFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Sizes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combos_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCombos_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCombos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numberphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModerationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountEndow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillDelivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivingType = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDelivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDelivery_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    ItemsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceEndow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoteType = table.Column<int>(type: "int", nullable: false),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillNotes_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillPayments_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartComboItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartComboItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartComboItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartComboItems_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProductItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProductItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProductItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProductItems_Sizes_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboItemsArchives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItemsArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboItemsArchives_BillDetails_BillDetailsId",
                        column: x => x.BillDetailsId,
                        principalTable: "BillDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboProductItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartComboId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboProductItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboProductItem_CartComboItems_CartComboId",
                        column: x => x.CartComboId,
                        principalTable: "CartComboItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboProductItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboProductItem_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AddressType", "Created", "CreatedBy", "District", "FullAddress", "FullName", "LastModified", "LastModifiedBy", "Latitude", "Longitude", "ModerationStatus", "NumberPhone", "Province", "SpecificAddress", "UserId", "Ward" },
                values: new object[] { new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Quận Nam Từ Liêm", "Số 36 Hàm Nghi, Phường Cầu Diễn, Quận Nam Từ Liêm, Thành phố Hà Nội", "BB Chicken-Hàm Nghi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 21.02983, 105.76913, 0, "055931234", "Thành phố Hà Nội", "Số 36 Hàm Nghi", null, "Phường Cầu Diễn" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "ModerationStatus" },
                values: new object[,]
                {
                    { new Guid("801ebf3e-d50c-48ec-998b-4f04ec7bfc3d"), "Combo gà", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222278_4.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("90dc4303-d8e3-4e08-99cd-fbfe73b5ef00"), "Mỳ ý", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://jollibee.com.vn//media/catalog/category/web-06.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"), "Gà sốt cay", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://jollibee.com.vn//media/catalog/category/web-07.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"), "Đồ uống", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://www.lotteria.vn/media/catalog/tmp/category/MENU_DAT_HANG_THU_C_UO_NG_new_3.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"), "Hamburger", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://jollibee.com.vn//media/catalog/category/cat_burger_1.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"), "Phần ăn phụ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://jollibee.com.vn//media/catalog/category/phananphu.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 },
                    { new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"), "Gà rán", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "LastModified", "LastModifiedBy", "ModerationStatus", "RoleName" },
                values: new object[,]
                {
                    { new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Quản trị viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Admin" },
                    { new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d2e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Quản trị viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Quản lý" },
                    { new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Nhân viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Nhân viên" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "AdditionalPrice", "Created", "CreatedBy", "DisplayOrder", "LastModified", "LastModifiedBy", "ModerationStatus", "ParentId", "SizeName" },
                values: new object[,]
                {
                    { new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, null, "Khoai tây chiên" },
                    { new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, null, "Đồ uống" },
                    { new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, null, "Hamburger" },
                    { new Guid("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"), 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, null, "Mỳ ý" }
                });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "Id", "BasePrice", "CategoryId", "ComboName", "Created", "CreatedBy", "Description", "ImageUrl", "LastModified", "LastModifiedBy", "ModerationStatus", "Quantity" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), 65000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"), "Combo Burger Siêu Cay", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Burger Siêu Cay kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, 0 },
                    { new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), 85000m, new Guid("801ebf3e-d50c-48ec-998b-4f04ec7bfc3d"), "Combo Gà Rán", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Gà Rán (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222281_4.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, 0 },
                    { new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), 87000m, new Guid("801ebf3e-d50c-48ec-998b-4f04ec7bfc3d"), "Combo Gà Nướng", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Gà Nướng (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/228380.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "Created", "CreatedBy", "Description", "ImageUrl", "LastModified", "LastModifiedBy", "ModerationStatus", "ProductName", "Quantity", "SizeId" },
                values: new object[,]
                {
                    { new Guid("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"), 35000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Burger Tôm", 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad") },
                    { new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 40000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Gà Rán (1 miếng)", 0, null },
                    { new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 15000m, new Guid("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/r/drink_pepsi_zero_m_l__2.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Pepsi Zero", 0, new Guid("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4") },
                    { new Guid("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"), 35000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_bulgogi_4.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Burger Bulgogi", 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad") },
                    { new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"), 25000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_shake_potato_tuy_t_xanh_.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Khoai lắc tuyết xanh", 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb") },
                    { new Guid("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"), 41000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_menu_5_.jpg.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Gà Sốt Bơ Tỏi (1 miếng)", 0, null },
                    { new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 40000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/227436_2.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Gà Nướng (1 miếng)", 0, null },
                    { new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 25000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_french_fries_m_i.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Khoai Tây Chiên (M)", 0, new Guid("0d41a8fd-f372-4c77-b5a3-63368e3994bb") },
                    { new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 35000m, new Guid("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Burger Siêu Cay", 0, new Guid("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad") },
                    { new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"), 41000m, new Guid("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0003_1.png.webp", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, "Gà Sốt HS (1 miếng)", 0, null }
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
                table: "Store",
                columns: new[] { "Id", "AddressId", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "ModerationStatus", "Status", "StoreName" },
                values: new object[] { new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"), new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, 0, "BB Chicken-Hàm Nghi" });

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
                table: "Users",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "FullName", "LastModified", "LastModifiedBy", "ModerationStatus", "Numberphone", "Password", "StoreId", "UserType" },
                values: new object[,]
                {
                    { new Guid("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "manhdb123@gmail.com", "Phạm Viết Mạnh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, null, "$2a$11$hfUavUdknuucYxRafesaoehNAZsDl825Ac/eYhsKn4dAA.fD./m8e", new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"), 1 },
                    { new Guid("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "admin@gmail.com", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, null, "$2a$11$AeE6SPxM54xLGVolLz4FOu85MVN5oIanvBf3DsdnMXji.u6fUHVMy", new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"), 1 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_StoreId",
                table: "Bill",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserId",
                table: "Bill",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDelivery_BillId",
                table: "BillDelivery",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillNotes_BillId",
                table: "BillNotes",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_BillId",
                table: "BillPayments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_CartComboItems_CartId",
                table: "CartComboItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartComboItems_ComboId",
                table: "CartComboItems",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItems_CartId",
                table: "CartProductItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItems_ProductId",
                table: "CartProductItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboItemsArchives_BillDetailsId",
                table: "ComboItemsArchives",
                column: "BillDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProductItem_CartComboId",
                table: "ComboProductItem",
                column: "CartComboId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProductItem_ProductId",
                table: "ComboProductItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboProductItem_SizeId",
                table: "ComboProductItem",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombos_ComboId",
                table: "ProductCombos",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombos_ProductId",
                table: "ProductCombos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ParentId",
                table: "Sizes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressId",
                table: "Store",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StoreId",
                table: "Users",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_UserId",
                table: "Address");

            migrationBuilder.DropTable(
                name: "BillDelivery");

            migrationBuilder.DropTable(
                name: "BillNotes");

            migrationBuilder.DropTable(
                name: "BillPayments");

            migrationBuilder.DropTable(
                name: "CartProductItems");

            migrationBuilder.DropTable(
                name: "ComboItemsArchives");

            migrationBuilder.DropTable(
                name: "ComboProductItem");

            migrationBuilder.DropTable(
                name: "ProductCombos");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartComboItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
