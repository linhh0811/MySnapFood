using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Service.SnapFood.Domain.Entitys;
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
        public Guid? SizeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; }
        public string CategoryName { get; set; }= string.Empty;
        public string? SizeName { get; set; } 
        public List<SizeDto>? Sizes { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public ModerationStatus CategoryModerationStatus { get; set; }
        public ModerationStatus SizeModerationStatus { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get;  set; }

        public Guid LastModifiedBy { get;  set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
    }
}
