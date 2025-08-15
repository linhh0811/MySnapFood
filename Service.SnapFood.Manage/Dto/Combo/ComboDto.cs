using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Combo
{
    public class ComboDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Phân loại không được để trống")]

        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;


        [Required(ErrorMessage ="Tên combo không được để trống")]
        public string ComboName { get; set; } = string.Empty;
        [Required(ErrorMessage ="Ảnh không được để trống")]
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }

        public DateTime CreteDate { get; set; }

        public ModerationStatus ModerationStatus { get; set; }
        public ModerationStatus CategoryModerationStatus { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }
        public List<ComboProductDto> ComboItems { get; set; } = new();
        public bool IsSelected { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
        public decimal PriceEndown { get; set; }

    }
}
