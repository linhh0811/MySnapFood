using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.Bill
{
    public class BillPaymentDto
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public PaymentType PaymentType { get; set; }
        public float Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
