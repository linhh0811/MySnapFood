using Service.SnapFood.Client.Dto.Combo;
using Service.SnapFood.Client.Dto.Promotion;

namespace Service.SnapFood.Client.Dto.Bill
{
    public class TopSellingItemDTo
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/images/default.png";
        public decimal BasePrice { get; set; }
        public decimal PriceEndown { get; set; }
        public decimal DiscountPercent { get; set; }      // % giảm

        public int TotalQuantity { get; set; }
        public string SizeName { get; set; } = string.Empty;
      
        public List<PromotionItemDto> PromotionItems { get; set; } = new List<PromotionItemDto>();

        public bool IsDangKM { get; set; }
        public List<ComboProductDto> Items { get; set; } = new();


    }
}
