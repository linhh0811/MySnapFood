using Service.SnapFood.Client.Dto.Addresss;
using Service.SnapFood.Client.Enums;
namespace Service.SnapFood.Client.Dto.Cart
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new AddressDto();
        public Status Status { get; set; }
        public DateTime? ThoiGianBatDauHoatDong { get; set; }
        public DateTime? ThoiGianNgungHoatDong { get; set; }

    }
}
