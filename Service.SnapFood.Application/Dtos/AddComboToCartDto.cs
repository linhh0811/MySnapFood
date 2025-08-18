using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class AddComboToCartDto
    {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }

        public Guid ComboId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
        public List<ComboProductItemDto> ProductSizes { get; set; } = new();
    }
}

