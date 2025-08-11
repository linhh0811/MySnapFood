using Service.SnapFood.Share.Model.Enum;
using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.Addresss
{
    public class AddressDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]

        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]

        public string NumberPhone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tỉnh/thành phố không được để trống")]

        public string Province { get; set; } = string.Empty;
        [Required(ErrorMessage = "Quận/huyện không được để trống")]

        public string District { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phường/xã không được để trống")]

        public string Ward { get; set; } = string.Empty;
        [Required(ErrorMessage = "Địa chỉ cụ thể không được để trống")]

        public string SpecificAddress { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FullAddress { get; set; } = string.Empty;
        public AddressType AddressType { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool IsDiaChiMacDinh { get; set; }


    }
}
