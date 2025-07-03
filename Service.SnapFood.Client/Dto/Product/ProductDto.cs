using Service.SnapFood.Client.Dto.Size;
using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.Product
{
    public class ProductDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }

        public string CategoryId { get; set; } = string.Empty;

        public string? SizeId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public decimal BasePrice { get; set; }

        public ModerationStatus ModerationStatus { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? SizeName { get; set; }
        public List<SizeDto>? Sizes { get; set; }

        public bool IsSelected { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
        public decimal PriceEndown { get; set; }
    }
}
