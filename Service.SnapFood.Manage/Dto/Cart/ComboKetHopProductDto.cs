using Service.SnapFood.Manage.Dto.Combo;
using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Cart
{
    public class ComboKetHopProductDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal PriceEndown { get; set; }
        public ItemType ItemType { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? SizeName { get; set; }

        public List<ComboProductDto> ComboItems { get; set; } = new();
    }
}
