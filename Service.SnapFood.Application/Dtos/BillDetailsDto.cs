using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public  class BillDetailsDto
    {
        public Guid Id { get; set; }
        public string ItemsName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEndow { get; set; }
        public ItemType ItemType { get; set; }
        public List<BillDetailsDto>? Children { get; set; }
    }
}
