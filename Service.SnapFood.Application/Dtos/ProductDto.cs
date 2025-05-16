using Microsoft.AspNetCore.Http;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class ProductDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SizeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
        public IFormFile Image { get; set; } = default!;
        public ModerationStatus ModerationStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get;  set; }

        public Guid LastModifiedBy { get;  set; }
    }
}
