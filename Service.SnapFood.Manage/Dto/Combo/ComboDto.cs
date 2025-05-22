using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Combo
{
    public class ComboDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }

        public string? CategoryId { get; set; }

        [Required(ErrorMessage ="Tên combo không được để trống")]
        public string ComboName { get; set; } = string.Empty;
        [Required(ErrorMessage ="Ảnh không được để trống")]
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public DateTime CreteDate { get; set; }

        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }
        public List<ComboProductDto> ComboItems { get; set; } = new();
    }
}
