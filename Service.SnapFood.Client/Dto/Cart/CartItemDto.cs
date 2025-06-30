using Service.SnapFood.Client.Dto.Combo;
using Service.SnapFood.Client.Enums;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Client.Dto.Cart
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal PriceEndown { get; set; }
        public int Quantity { get; set; }

        public List<ComboProductDto> ComboItems { get; set; } = new();
        public string? CategoryName { get; set; }

        public string? SizeName { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created;
    }
}
