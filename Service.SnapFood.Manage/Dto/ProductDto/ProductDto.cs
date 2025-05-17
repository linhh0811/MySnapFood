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

        public string? CategoryId { get; set; }

        public string? SizeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không để trống.")]
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Giá sản phẩm không để trống.")]

        public decimal BasePrice { get; set; }
        public IBrowserFile Image { get; set; } = default!;
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid LastModifiedBy { get; set; }
    }
}
