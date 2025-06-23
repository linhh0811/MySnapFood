using Service.SnapFood.Client.Dto.Combo;
using Service.SnapFood.Share.Model.Enum;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Client.Dto.Promotion
{
    public class PromotionItemDto
    {
        public Guid Id { get; set; }
        public Guid PromotionId { get; set; }
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        //Thêm
        public string ItemName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public List<ComboProductDto> ComboItems { get; set; } = new();
        public bool IsSelected { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public string? CategoryName { get; set; }

        public string? SizeName { get; set; }
    }
}
