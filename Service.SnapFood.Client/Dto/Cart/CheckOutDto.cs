using Service.SnapFood.Client.Enums;

namespace Service.SnapFood.Client.Dto.Cart
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
