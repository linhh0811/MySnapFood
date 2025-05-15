using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Domain.Entitys
{
    public class Bill : IntermediaryEntity
    {
        public string BillCode { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public StatusOrder Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual BillDelivery BillDelivery { get; set; } = null!;
        public virtual List<BillDetails> BillDetails { get; set; } = null!;
        public virtual List<BillPayment> BillPayments { get; set; } = null!;
        public virtual List<BillNotes> BillNotes { get; set; } = null!;
    }
}
