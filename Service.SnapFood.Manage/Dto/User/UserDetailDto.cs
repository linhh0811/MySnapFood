using Service.SnapFood.Manage.Dto.Bill;
using System;
using System.Collections.Generic;

namespace Service.SnapFood.Manage.Dto.UserDto
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<BillDto> Bills { get; set; } = new List<BillDto>();
    }
}