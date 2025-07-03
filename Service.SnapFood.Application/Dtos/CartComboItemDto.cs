using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class CartComboItemDto
    {
        public Guid Id { get; set; } // Thêm Id để xóa và cập nhật
        public Guid ComboId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public string SizeName { get; set; } = string.Empty; // Thêm để hiển thị kích thước
        public string ImageUrl { get; set; } = string.Empty; // Thêm để hiển thị hình ảnh
        public decimal Price { get; set; } // Thêm để hiển thị giá
        public List<ComboProductItemDto> ComboProductItems { get; set; } = new List<ComboProductItemDto>();
    }
}
