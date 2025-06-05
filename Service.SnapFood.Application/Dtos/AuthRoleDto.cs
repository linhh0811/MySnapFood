using Service.SnapFood.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Dtos
{
    public class AuthRoleDto
    {
        public string RoleName { get; set; } = string.Empty;

        public EnumRole EnumRole { get; set; } 
    }
}
