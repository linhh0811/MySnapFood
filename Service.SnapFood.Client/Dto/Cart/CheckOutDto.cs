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
    }
}
