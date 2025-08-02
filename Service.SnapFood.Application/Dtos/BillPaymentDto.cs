using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class BillPaymentDto
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public StatusPayment PaymentStatus { get; set; }
    }
}
