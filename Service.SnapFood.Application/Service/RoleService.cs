using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Roles>> GetAllRolesAsync()
        {
            return (await _unitOfWork.RolesRepo.GetAllAsync()).ToList();
        }

        // Phương thức mới: Lấy danh sách tài khoản Store với trạng thái IsInRole
        public DataTableJson GetAllUsersPagedForRole(Guid roleId, BaseQuery query)
        {
            if (query == null || query.gridRequest == null)
                throw new ArgumentNullException(nameof(query), "Thông tin phân trang không hợp lệ");

            int totalRecords = 0;
            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q.Where(u => u.UserType == UserType.Store), // Chỉ lấy tài khoản Store
                query.gridRequest,
                ref totalRecords
            );

            // Lấy danh sách userId thuộc quyền đang chọn
            var userIdsInRole = _unitOfWork.UserRoleRepo.FindWhere(ur => ur.RoleId == roleId)
                .Select(ur => ur.UserId)
                .ToHashSet();

            var data = dataQuery.AsEnumerable()
                .Select((u, i) => new UserDto
                {
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    IsInRole = userIdsInRole.Contains(u.Id) // Gán trạng thái IsInRole
                });

            return new DataTableJson(data, query.draw, totalRecords);
        }

        // Phương thức cũ: Giữ nguyên để tương thích với các chức năng khác nếu cần
        public DataTableJson GetAllUsersPaged(BaseQuery query)
        {
            if (query == null || query.gridRequest == null)
                throw new ArgumentNullException(nameof(query), "Thông tin phân trang không hợp lệ");

            int totalRecords = 0;
            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q.Where(u => u.UserType == UserType.Store), // Chỉ lấy tài khoản Store
                query.gridRequest,
                ref totalRecords
            );

            var data = dataQuery.AsEnumerable()
                .Select((u, i) => new UserDto
                {
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email
                });

            return new DataTableJson(data, query.draw, totalRecords);
        }

        public List<User> GetUsersByRoleId(Guid roleId)
        {
            var userRoles = _unitOfWork.UserRoleRepo.FindWhere(ur => ur.RoleId == roleId);
            var userIds = userRoles.Select(ur => ur.UserId).ToList();
            var users = _unitOfWork.UserRepo.FindWhere(u => userIds.Contains(u.Id) && u.UserType == UserType.Store); // Chỉ lấy Store
            return users.ToList();
        }

        public DataTableJson GetUsersByRoleIdPaged(Guid roleId, BaseQuery query)
        {
            if (query == null || query.gridRequest == null)
                throw new ArgumentNullException(nameof(query), "Thông tin phân trang không hợp lệ");

            int totalRecords = 0;
            var userRoles = _unitOfWork.UserRoleRepo.FindWhere(ur => ur.RoleId == roleId);
            var userIds = userRoles.Select(ur => ur.UserId).ToList();

            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q.Where(u => userIds.Contains(u.Id) && u.UserType == UserType.Store), // Chỉ lấy Store
                query.gridRequest,
                ref totalRecords
            );

            var data = dataQuery.AsEnumerable()
                .Select((u, i) => new UserDto
                {
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email
                });

            return new DataTableJson(data, query.draw, totalRecords);
        }

        public async Task<bool> AddUserToRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if (user == null || user.UserType != UserType.Store)
                throw new Exception("Người dùng không hợp lệ hoặc không phải tài khoản Store");

            var existingUserRole = _unitOfWork.UserRoleRepo.FindWhere(ur => ur.UserId == userId && ur.RoleId == roleId).FirstOrDefault();
            if (existingUserRole != null)
                throw new Exception("Người dùng đã được gán vào quyền này");

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            await _unitOfWork.UserRoleRepo.AddAsync(userRole);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromRoleAsync(Guid userId, Guid roleId)
        {
            var existingUserRole = _unitOfWork.UserRoleRepo.FindWhere(ur => ur.UserId == userId && ur.RoleId == roleId).FirstOrDefault();
            if (existingUserRole == null)
                throw new Exception("Người dùng không thuộc quyền này");

            _unitOfWork.UserRoleRepo.Delete(existingUserRole);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}