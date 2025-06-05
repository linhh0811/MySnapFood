using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.Combo
{
    public class ComboDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }

        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        public string ComboName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }

        public ModerationStatus ModerationStatus { get; set; }

        public List<ComboProductDto> ComboItems { get; set; } = new();
    }
}
