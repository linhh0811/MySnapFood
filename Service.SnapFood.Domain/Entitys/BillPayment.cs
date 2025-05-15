using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Domain.Entitys
{
    public class BillPayment : IntermediaryEntity
    {
        public Guid BillId { get; set; }
        public PaymentType PaymentType { get; set; }
        public float Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public StatusPayment PaymentStatus { get; set; }
        public virtual Bill Bill { get; set; } = null!;
    }

}
