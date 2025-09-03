﻿using System.ComponentModel.DataAnnotations;

namespace Service.SnapFood.Client.Dto.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; } = string.Empty;
        public string RecaptchaToken { get; set; } = string.Empty;
    }
}
