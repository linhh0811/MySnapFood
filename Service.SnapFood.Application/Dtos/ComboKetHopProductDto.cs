using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class ComboKetHopProductDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal PriceEndown { get; set; }
        public ItemType ItemType { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? SizeName { get; set; } 

        public List<ComboProductDto> ComboItems { get; set; } = new();
    }
}
