using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class CheckOutDto
    {
        public Guid UserId { get; set; }
        public ReceivingType ReceivingType { get; set; }
        public PaymentType PaymentType { get; set; }
        public string ReceiverName { get; set; } = string.Empty;
        public string ReceiverPhone { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid DiscountCodeId { get; set; }
        public decimal DiscountCodeValue { get; set; }
        public decimal PhiGiaoHang { get; set; }
        public double KhoangCach { get; set; }

    }
}
