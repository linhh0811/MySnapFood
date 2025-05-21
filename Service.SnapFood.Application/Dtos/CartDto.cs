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
        public Guid ProductOrComboId { get; set; } 
        public int Quantity { get; set; }
    }
}
