using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Infrastructure.Configurations;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.EF.Contexts
{
    public class AppDbContext : AuditableDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
            builder.Entity<Sizes>().HasData(new Sizes
            {
                Id = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                SizeName = "Đồ uống",
                AdditionalPrice = 0,
                ParentId = null,
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=1
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "M",
                AdditionalPrice = 0,
                ParentId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=1
            }, new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "L",
                AdditionalPrice = 4000,
                ParentId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=2

            }, new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "XL",
                AdditionalPrice = 7000,
                ParentId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=3
            },
            new Sizes
            {
                Id = Guid.Parse("0d41a8fd-f372-4c77-b5a3-63368e3994bb"),
                SizeName = "Khoai tây chiên",
                AdditionalPrice = 0,
                ParentId = null,
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=2
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Nhỏ",
                AdditionalPrice = 0,
                ParentId = Guid.Parse("0d41a8fd-f372-4c77-b5a3-63368e3994bb"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=1
            }, new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Vừa",
                AdditionalPrice = 7000,
                ParentId = Guid.Parse("0d41a8fd-f372-4c77-b5a3-63368e3994bb"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=2
            }, new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Lớn",
                AdditionalPrice = 15000,
                ParentId = Guid.Parse("0d41a8fd-f372-4c77-b5a3-63368e3994bb"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=3
            },
            new Sizes
            {
                Id = Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                SizeName = "Hamburger",
                AdditionalPrice = 0,
                ParentId = null,
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=3
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Tiêu chuẩn",
                AdditionalPrice = 0,
                ParentId = Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=1
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Big",
                AdditionalPrice = 10000,
                ParentId = Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=2
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Mega",
                AdditionalPrice = 25000,
                ParentId = Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=3
            },
            new Sizes
            {
                Id = Guid.Parse("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"),
                SizeName = "Mỳ ý",
                AdditionalPrice = 0,
                ParentId = null,
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=4
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Phần thường",
                AdditionalPrice = 0,
                ParentId = Guid.Parse("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=1
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Phần lớn",
                AdditionalPrice = 12000,
                ParentId = Guid.Parse("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=2
            },
            new Sizes
            {
                Id = Guid.NewGuid(),
                SizeName = "Phần đại",
                AdditionalPrice = 25000,
                ParentId = Guid.Parse("8e1e9e3c-82e5-4142-b987-c39c8de69c0e"),
                ModerationStatus = ModerationStatus.Approved,
                DisplayOrder=3
            }
            );
            builder.Entity<Categories>().HasData(new Categories
            {
                Id = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                CategoryName = "Đồ uống",
                DisplayOrder = 5,
                ImageUrl = "https://jollibee.com.vn//media/catalog/category/thucuong.png",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Categories
            {
                Id = Guid.Parse("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"),//Gà sốt cay
                CategoryName = "Gà sốt cay",
                DisplayOrder = 1,
                ImageUrl = "https://jollibee.com.vn//media/catalog/category/web-07.png",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Categories
            {
                Id = Guid.Parse("90dc4303-d8e3-4e08-99cd-fbfe73b5ef00"),
                CategoryName = "Mỳ ý",
                DisplayOrder = 2,
                ImageUrl = "https://jollibee.com.vn//media/catalog/category/web-06.png",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Categories
            {
                Id = Guid.Parse("801ebf3e-d50c-48ec-998b-4f04ec7bfc3d"),
                CategoryName = "Combo gà",
                DisplayOrder=7,
                ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222278_4.png.webp",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Categories
            {
                Id = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"),//Gà rán
                CategoryName = "Gà rán",
                DisplayOrder = 3,
                ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Categories
            {
                Id = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"),//Hamburger
                CategoryName = "Hamburger",
                DisplayOrder = 4,
                ImageUrl = "https://jollibee.com.vn//media/catalog/category/cat_burger_1.png",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Categories
            {
                Id = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"),//Phần ăn phụ
                CategoryName = "Phần ăn phụ",
                DisplayOrder = 6,
                ImageUrl = "https://jollibee.com.vn//media/catalog/category/phananphu.png",
                ModerationStatus = ModerationStatus.Approved,
            }

            );
            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = Guid.Parse("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"),
                   CategoryId=Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"),//Hamburger
                   SizeId =Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                   ProductName = "Burger Siêu Cay",
                   Description = "Burger Siêu Cay",
                   Quantity = 0,
                   BasePrice = 35000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp"
               },
               new Product
               {
                   Id = Guid.Parse("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"),
                   ProductName = "Burger Bulgogi",
                   CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"),//Hamburger
                   SizeId = Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                   Description = "Burger Bulgogi",
                   Quantity = 0,
                   BasePrice = 35000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_bulgogi_4.png.webp"
               },
               new Product
               {
                   Id = Guid.Parse("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"),
                   ProductName = "Burger Tôm",
                   CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"),//Hamburger
                   SizeId = Guid.Parse("607f96c3-b3dc-4db3-8f5e-19b6e07cbcad"),
                   Description = "Burger Tôm",
                   Quantity = 0,
                   BasePrice = 35000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp"
               },
                new Product
                {
                    Id = Guid.Parse("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ae"),
                    ProductName = "Burger Double Double",
                    CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"),//Hamburger
                    Description = "Burger Double Double",
                    Quantity = 0,
                    BasePrice = 55000,
                    ModerationStatus = ModerationStatus.Approved,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp"
                },
               new Product
               {
                   Id = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"),
                   CategoryId = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                   SizeId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                   ProductName = "Pepsi Zero",
                   Description = "Pepsi Zero",
                   Quantity = 0,
                   BasePrice = 15000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/r/drink_pepsi_zero_m_l__2.png.webp"
               },
                new Product
                {
                    Id = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-84647eb07bfc"),
                    CategoryId = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                    SizeId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                    ProductName = "Trà chanh hạt chia",
                    Description = "Trà chanh hạt chia",
                    Quantity = 0,
                    BasePrice = 20000,
                    ModerationStatus = ModerationStatus.Approved,
                    ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/h/_/h_nh_s_n_ph_m.png"
                },
                 new Product
                 {
                     Id = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-84647eb07bfd"),
                     CategoryId = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                     SizeId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                     ProductName = "Mirinda ",
                     Description = "Mirinda",
                     Quantity = 0,
                     BasePrice = 15000,
                     ModerationStatus = ModerationStatus.Approved,
                     ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_7_8_1.png"
                 },
                 new Product
                 {
                     Id = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-84647eb07bfe"),
                     CategoryId = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                     SizeId = Guid.Parse("1c3d519b-04e4-42c3-a86d-7a7db6e9a7a4"),
                     ProductName = "7 Up ",
                     Description = "7 Up",
                     Quantity = 0,
                     BasePrice = 15000,
                     ModerationStatus = ModerationStatus.Approved,
                     ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_9_10.png"
                 },
                 new Product
                 {
                     Id = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-84647eb07bff"),
                     CategoryId = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                     ProductName = "Nước suối ",
                     Description = "Nước suối",
                     Quantity = 0,
                     BasePrice = 10000,
                     ModerationStatus = ModerationStatus.Approved,
                     ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_1th_c_u_ng_-_2.png"
                 },
                  new Product
                  {
                      Id = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-84647eb08bff"),
                      CategoryId = Guid.Parse("b5b3cc50-ec70-4093-9d10-4c7b0c73f9ca"),//Đồ uống
                      ProductName = "MILKIS DELI ",
                      Description = "MILKIS DELI",
                      Quantity = 0,
                      BasePrice = 22000,
                      ModerationStatus = ModerationStatus.Approved,
                      ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_-_milkis_menu_web.jpg.webp"
                  },
               new Product
               {
                   Id = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"),
                   ProductName = "Khoai Tây Chiên",
                   CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"),//Phần ăn phụ
                   SizeId = Guid.Parse("0d41a8fd-f372-4c77-b5a3-63368e3994bb"),
                   Description = "Khoai Tây Chiên",
                   Quantity = 0,
                   BasePrice = 25000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_french_fries_m_i.png.webp"
               },
               new Product
               {
                   Id = Guid.Parse("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"),
                   CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"),//Phần ăn phụ
                   SizeId = Guid.Parse("0d41a8fd-f372-4c77-b5a3-63368e3994bb"),
                   ProductName = "Khoai lắc tuyết xanh",
                   Description = "Mô tả",
                   Quantity = 0,
                   BasePrice = 25000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_shake_potato_tuy_t_xanh_.png.webp"
               },
                new Product
                {
                    Id = Guid.Parse("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"),
                    CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"),//Phần ăn phụ
                    ProductName = "Súp bí đỏ",
                    Description = "Súp bí đỏ",
                    Quantity = 0,
                    BasePrice = 15000,
                    ModerationStatus = ModerationStatus.Approved,
                    ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_5.png"
                },
                new Product
                {
                    Id = Guid.Parse("b487da52-d738-4376-a1e3-c4a4d2fc7ef3"),
                    CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc5"),//Phần ăn phụ
                    ProductName = "Cơm trắng",
                    Description = "Cơm trắng",
                    Quantity = 0,
                    BasePrice = 10000,
                    ModerationStatus = ModerationStatus.Approved,
                    ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_6.png"
                },
               new Product
               {
                   Id = Guid.Parse("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"),
                   ProductName = "Gà sốt HS(1 miếng)",
                   CategoryId = Guid.Parse("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"),//Gà sốt cay
                   SizeId =null,
                   Description = "Gà sốt HS(1 miếng)",
                   Quantity = 0,
                   BasePrice = 35000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_7-compressed.jpg"
               },
                new Product
                {
                    Id = Guid.Parse("f6a71ac8-78f3-4194-88c9-c2aa9467f94e"),
                    ProductName = "Gà sốt cay(2 miếng)",
                    CategoryId = Guid.Parse("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"),//Gà sốt cay
                    SizeId = null,
                    Description = "Gà sốt cay(2 miếng)",
                    Quantity = 0,
                    BasePrice = 69000,
                    ModerationStatus = ModerationStatus.Approved,
                    ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_6-compressed_1.jpg"
                },
                 new Product
                 {
                     Id = Guid.Parse("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"),
                     ProductName = "Cơm gà sốt cay",
                     CategoryId = Guid.Parse("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"),//Gà sốt cay
                     SizeId = null,
                     Description = "Cơm gà sốt cay",
                     Quantity = 0,
                     BasePrice = 49000,
                     ModerationStatus = ModerationStatus.Approved,
                     ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_5-compressed.jpg"
                 },
               new Product
               {
                   Id = Guid.Parse("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"),
                   ProductName = "Gà Nướng (1 miếng)",
                   CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"),//Gà rán
                   SizeId = null,
                   Description = "Gà Nướng (1 miếng)",
                   Quantity = 0,
                   BasePrice = 40000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/227436_2.png.webp"
               },
               new Product
               {
                   Id = Guid.Parse("7b17b539-8168-42c5-8b9f-1c1c783bd423"),
                   ProductName = "Gà Rán (1 miếng)",
                   CategoryId   = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"),//Gà rán
                   SizeId = null,
                   Description = "Gà Rán (1 miếng)",
                   Quantity = 0,
                   BasePrice = 40000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp"
               },
               new Product
               {
                   Id = Guid.Parse("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"),
                   ProductName = "Gà Sốt Bơ Tỏi (1 miếng)",
                   CategoryId = Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc6"),//Gà rán
                   SizeId = null,
                   Description = "Gà Sốt Bơ Tỏi (1 miếng)",
                   Quantity = 0,
                   BasePrice = 41000,
                   ModerationStatus = ModerationStatus.Approved,
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_menu_5_.jpg.webp"
               }
               );
            builder.Entity<Combo>().HasData(
                 new Combo
                 {
                     Id = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ac"),
                     CategoryId = Guid.Parse("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"),//Gà sốt cay
                     ComboName = "Combo cơm gà sốt cay",
                     ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_4-compressed.jpg",
                     BasePrice = 60000,
                     ModerationStatus = ModerationStatus.Approved,
                     Description = "Cơm Gà Sốt Cay + Nước ngọt",
                 },
                 new Combo
                 {
                     Id = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ad"),
                     CategoryId = Guid.Parse("aeb6acbb-2490-4d20-b6b4-3e15c1e878c8"),//Gà sốt cay
                     ComboName = "Combo cơm gà sốt cay",
                     ImageUrl = "https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_3-compressed.jpg",
                     BasePrice = 70000,
                     ModerationStatus = ModerationStatus.Approved,
                     Description = "Cơm Gà Sốt Cay +Súp bí đỏ+ Nước ngọt",
                 },
               new Combo
               {
                   Id = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                   CategoryId= Guid.Parse("eeddb184-0a25-40a4-9e8f-98e905fc4dc4"),//Hamburger
                   ComboName = "Combo Burger Siêu Cay",
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp",
                   BasePrice = 65000,
                   ModerationStatus = ModerationStatus.Approved,
                   Description = "Burger Siêu Cay kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
               },
               new Combo
               {
                   Id = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                   ComboName = "Combo Gà Rán",
                   CategoryId = Guid.Parse("801ebf3e-d50c-48ec-998b-4f04ec7bfc3d"),//Combo gà
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222281_4.png.webp",
                   BasePrice = 85000,
                   ModerationStatus = ModerationStatus.Approved,
                   Description = "Gà Rán (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
               },
               new Combo
               {
                   Id = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                   CategoryId= Guid.Parse("801ebf3e-d50c-48ec-998b-4f04ec7bfc3d"),//Combo gà
                   ComboName = "Combo Gà Nướng",
                   ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/228380.png.webp",
                   BasePrice = 87000,
                   ModerationStatus = ModerationStatus.Approved,
                   Description = "Gà Nướng (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
               }
               );
            builder.Entity<ProductCombo>().HasData(
                new ProductCombo
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.Parse("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), // Cơm gà sốt Siêu Cay
                    ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ad"),//Combo cơm gà sốt cay
                    Quantity = 1,
                },
                  new ProductCombo
                  {
                      Id = Guid.NewGuid(),
                      ProductId = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero 
                      ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ad"),//Combo cơm gà sốt cay
                      Quantity = 1,
                  },
                   new ProductCombo
                   {
                       Id = Guid.NewGuid(),
                       ProductId = Guid.Parse("b487da52-d738-4376-a1e3-c4a4d2fc7ef2"), // súp bí đỏ 
                       ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ad"),//Combo cơm gà sốt cay
                       Quantity = 1,
                   },
                 new ProductCombo
                 {
                     Id = Guid.NewGuid(),
                     ProductId = Guid.Parse("f6a71ac8-78f3-4194-88c9-c2aa9467f95e"), // Cơm gà sốt Siêu Cay
                     ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ac"),//Combo cơm gà sốt cay
                     Quantity = 1,
                 },
                  new ProductCombo
                  {
                      Id = Guid.NewGuid(),
                      ProductId = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero 
                      ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ac"),//Combo cơm gà sốt cay
                      Quantity = 1,
                  },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), // Burger Siêu Cay
                   ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                   Quantity = 1,
               },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), // Khoai Tây Chiên 
                   ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                   Quantity = 1,
               },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero 
                   ComboId = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                   Quantity = 1,
               },

               // Combo Gà Rán
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("7b17b539-8168-42c5-8b9f-1c1c783bd423"), // Gà Rán (1 miếng)
                   ComboId = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                   Quantity = 1,
               },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), // Khoai Tây Chiên (M)
                   ComboId = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                   Quantity = 1,
               },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero 
                   ComboId = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                   Quantity = 1,
               },

               // Combo Gà Nướng
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), // Gà Nướng (1 miếng)
                   ComboId = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                   Quantity = 1,
               },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), // Khoai Tây Chiên (M)
                   ComboId = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                   Quantity = 1,
               },
               new ProductCombo
               {
                   Id = Guid.NewGuid(),
                   ProductId = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero (M)
                   ComboId = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                   Quantity = 1,
               }
               );
            builder.Entity<Address>().HasData(
                new Address
                {
                    Id = Guid.Parse("931F07E5-46D8-4449-B77E-533BF4F33AA3"),
                    UserId = null,
                    FullName = "BB Chicken-Hàm Nghi",
                    NumberPhone = "055931234",
                    Province = "Thành phố Hà Nội",
                    District = "Quận Nam Từ Liêm",
                    Ward = "Phường Cầu Diễn",
                    SpecificAddress = "Số 36 Hàm Nghi",
                    FullAddress = "Số 36 Hàm Nghi, Phường Cầu Diễn, Quận Nam Từ Liêm, Thành phố Hà Nội",
                    Latitude = 21.02983,
                    Longitude = 105.76913,
                    AddressType = AddressType.Store,
                    ModerationStatus = ModerationStatus.Approved,
                }
                );
            builder.Entity<Store>().HasData(
                new Store
                {
                    Id = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                    StoreName = "BB Chicken-Hàm Nghi",
                    Status = Status.Activity,
                    AddressId = Guid.Parse("931F07E5-46D8-4449-B77E-533BF4F33AA3"),
                }

                );
            builder.Entity<Roles>().HasData(new Roles
            {
                Id = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"),
                RoleName = "Admin",
                EnumRole=EnumRole.Admin,
                Description = "Quản trị viên",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Roles
            {
                Id = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d2e"),
                RoleName = "Quản lý",
                EnumRole = EnumRole.Manager,
                Description = "Quản trị viên",
                ModerationStatus = ModerationStatus.Approved,
            },
            new Roles
            {
                Id = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"),
                RoleName = "Nhân viên",
                EnumRole = EnumRole.Staff,
                Description = "Nhân viên",
                ModerationStatus = ModerationStatus.Approved,
            });
            builder.Entity<User>().HasData(
               new User
               {
                   Id = Guid.Parse("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                   StoreId = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                   FullName = "Admin",
                   Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                   Email = "admin@gmail.com",
                   UserType = UserType.Store,

               },
               new User
               {
                   Id = Guid.Parse("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                   StoreId = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                   FullName = "Phạm Viết Mạnh",
                   Password = BCrypt.Net.BCrypt.HashPassword("manhdb123"),
                   Email = "manhdb123@gmail.com",
                   UserType = UserType.Store,
               }

               );
            builder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                    RoleId = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d1e"),
                },
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("8a2e5d21-5f5b-4a7c-9d5e-3f6c8b2a1d5e"),
                    RoleId = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"),
                },
                 new UserRole
                 {
                     Id = Guid.NewGuid(),
                     UserId = Guid.Parse("8a2e5d21-5f7b-4a7c-9d5e-3f6c8b2a1d4e"),
                     RoleId = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d3e"),
                 });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProductItem> CartProductItems { get; set; }
        public DbSet<CartComboItem> CartComboItems { get; set; } // Thêm DbSet cho CartComboItem
        public DbSet<ComboProductItem> ComboProductItems { get; set; } // Thêm DbSet cho ComboProductItem
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetails> BillDetailses { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCombo> productCombos { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }

        public DbSet<BillDelivery> BillDeliveries { get; set; }
        public DbSet<BillNotes> BillNotes { get; set; }
        public DbSet<BillPayment> BillPayments { get; set; }
        public DbSet<ComboItemsArchive> ComboItemsArchives { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<PromotionItem> PromotionItems { get; set; }
        public DbSet<Promotions> Promotions { get; set; }


    }
}
