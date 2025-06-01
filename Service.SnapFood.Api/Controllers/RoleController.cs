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

        [HttpPost("{roleId}/Users/GetPaged")]
        public IActionResult GetUsersByRoleId(Guid roleId, [FromBody] BaseQuery query)
        {
            try
            {
                var result = _roleService.GetUsersByRoleIdPaged(roleId, query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{roleId}/AddUser")]
        public async Task<IActionResult> AddUserToRole(Guid roleId, [FromBody] Guid userId)
        {
            try
            {
                var result = await _roleService.AddUserToRoleAsync(userId, roleId);
                return Ok("Thêm người dùng vào quyền thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}