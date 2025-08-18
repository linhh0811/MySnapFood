using Service.SnapFood.Client.Enums;
using Service.SnapFood.Share.Model.SQL;
using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^(0[2-9][0-9]{8,9})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Numberphone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordMoi { get; set; } = string.Empty;
        public string PasswordConfirmMoi { get; set; } = string.Empty;
        public bool IsThayDoiMatKhau { get; set; } = false;

    }
}
