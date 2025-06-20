using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartProductItemDto> CartProductItems { get; set; } = new List<CartProductItemDto>();
        public List<CartComboItemDto> CartComboItems { get; set; } = new List<CartComboItemDto>();
    }
}
