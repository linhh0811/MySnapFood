using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<Roles>> GetAllRolesAsync();
        DataTableJson GetAllUsersPagedForRole(Guid roleId, BaseQuery query);
        List<User> GetUsersByRoleId(Guid roleId);
        DataTableJson GetAllUsersPaged(BaseQuery query);
        DataTableJson GetUsersByRoleIdPaged(Guid roleId, BaseQuery query);
        Task<bool> AddUserToRoleAsync(Guid userId, Guid roleId);
        Task<bool> RemoveUserFromRoleAsync(Guid userId, Guid roleId);
    }
}