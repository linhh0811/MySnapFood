using Service.SnapFood.Manage.Enums;

namespace Service.SnapFood.Manage.Dto.Bill
{
    public class BillDeliveryDto
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public ReceivingType ReceivingType { get; set; } //phương thức nhận hàng
        public string ReceiverName { get; set; } = string.Empty; //tên người nhận
        public string ReceiverPhone { get; set; } = string.Empty; //số điện thoại người nhận
        public string ReceiverAddress { get; set; } = string.Empty; //địa chỉ người nhận
        public decimal Distance { get; set; } //Khoảng cách
        public decimal DeliveryFee { get; set; }//Phí giao hàng
    }
}
