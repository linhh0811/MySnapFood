using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public  class TopSellingItemDto:ProductDto
    {


        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/images/default.png";

        public decimal BasePrice { get; set; }       
        public decimal PriceEndown { get; set; }      
        public decimal DiscountPercent { get; set; }    
        public string CategoryName { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public string SizeName { get; set; } = string.Empty;
   

       
        public bool IsDangKM { get; set; }
        public List<ComboProductDto> Items { get; set; } = new();

      

        public string DisplayCategory { get; set; }



    }
}
