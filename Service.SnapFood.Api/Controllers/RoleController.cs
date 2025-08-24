using Microsoft.AspNetCore.Mvc;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Share.Query;
using System;
using System.Threading.Tasks;

namespace Service.SnapFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost("Users/GetPaged")]
        public IActionResult GetAllUsersPaged([FromBody] BaseQuery query)
        {

            var result = _roleService.GetAllUsersPaged(query);
            return Ok(result);

        }

        [HttpPost("{roleId}/Users/GetPaged")]
        public IActionResult GetUsersByRoleId(Guid roleId, [FromBody] BaseQuery query)
        {

            var result = _roleService.GetUsersByRoleIdPaged(roleId, query);
            return Ok(result);

        }

        [HttpPost("{roleId}/GetAllUsersPaged")]
        public IActionResult GetAllUsersPagedForRole(Guid roleId, [FromBody] BaseQuery query)
        {

            var result = _roleService.GetAllUsersPagedForRole(roleId, query);
            return Ok(result);

        }

        [HttpPost("{roleId}/AddUser")]
        public async Task<IActionResult> AddUserToRole(Guid roleId, [FromBody] Guid userId)
        {

            var result = await _roleService.AddUserToRoleAsync(userId, roleId);
            return Ok(new { success = true, message = "Thêm người dùng vào quyền thành công" });

        }

        [HttpPost("{roleId}/RemoveUser")]
        public async Task<IActionResult> RemoveUserFromRole(Guid roleId, [FromBody] Guid userId)
        {

            var result = await _roleService.RemoveUserFromRoleAsync(userId, roleId);
            return Ok(new { success = true, message = "Xóa người dùng khỏi quyền thành công" });

        }
    }
}