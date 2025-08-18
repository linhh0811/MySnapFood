using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class AddProductToCartDto
    {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
