using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Share.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Entitys
{
    public class Roles : BaseDomainEntity
    {
        public string RoleName { get; set; } = string.Empty;
        public EnumRole EnumRole { get; set; }

        public string Description { get; set; } = string.Empty;
        public virtual List<UserRole> UserRoles { get; set; } = null!;
    }
}
