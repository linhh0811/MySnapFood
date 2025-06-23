using Microsoft.AspNetCore.Components.Forms;
using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.ProductDto
{
    public class ProductDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Phân loại sản phẩm không để trống.")]

        public string CategoryId { get; set; } = string.Empty;

        public string? SizeId { get; set; }
        [Required(ErrorMessage = "Ảnh không để trống.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không để trống.")]
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Giá sản phẩm không để trống.")]
        [Range(1,int.MaxValue,ErrorMessage ="Giá sản phẩm lớn hơn 0")]
        public decimal BasePrice { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public ModerationStatus CategoryModerationStatus { get; set; }
        public ModerationStatus SizeModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public string? SizeName { get; set; }
        public bool IsSelected { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
    }
}
