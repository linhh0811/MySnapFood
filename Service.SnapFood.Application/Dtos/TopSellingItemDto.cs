using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public  class TopSellingItemDto:ProductDto
    {


        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/images/default.png";

        public decimal BasePrice { get; set; }        // Giá gốc
        public decimal PriceEndown { get; set; }      // Giá sau giảm
        public decimal DiscountPercent { get; set; }      // % giảm

        public int TotalQuantity { get; set; }
        public string SizeName { get; set; } = string.Empty;

        public List<PromotionItemDto> PromotionItems { get; set; } = new List<PromotionItemDto>();

        public bool IsDangKM { get; set; }
        public List<ComboProductDto> Items { get; set; } = new();


    }
}
