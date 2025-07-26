using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Bill
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
