using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class ComboProductItemDto
    {
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SizeName { get; set; } = string.Empty;
    }
}
