using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class QuantityInCartDto
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int QuantityThayDoi { get; set; }
    }
}
