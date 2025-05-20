using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Category
{
    public class CategoryDto
    {
        public string? Id { get; set; }
        public int Index { get; set; }
        [Required(ErrorMessage = "Tên phân loại không để trống.")]
        public string CategoryName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ảnh phân loại để trống.")]
        public string ImageUrl { get; set; } = string.Empty;
        public ModerationStatus ModerationStatus { get; set; }

    }
}
