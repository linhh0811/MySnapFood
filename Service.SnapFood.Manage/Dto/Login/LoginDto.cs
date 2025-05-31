using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Manage.Dto.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email không được để trống")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; } = string.Empty;
    }
}
