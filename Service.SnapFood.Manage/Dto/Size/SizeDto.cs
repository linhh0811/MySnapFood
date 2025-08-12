using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Size
{
    public class SizeDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="Tên size không được để trống")]
        public string SizeName { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; }
        public int DisplayOrder { get; set; }
        public Guid? ParentId { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
    }
}
