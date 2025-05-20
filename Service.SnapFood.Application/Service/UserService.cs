using BCrypt.Net;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Duyệt và Hủy duyệt
        public async Task<bool> ApproveAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user != null)
            {
                user.ModerationStatus = ModerationStatus.Approved;
                _unitOfWork.UserRepo.Update(user);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user != null)
            {
                user.ModerationStatus = ModerationStatus.Rejected;
                _unitOfWork.UserRepo.Update(user);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
        #endregion

        #region Lấy dữ liệu
        public async Task<List<User>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            return users.ToList();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            return user;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            int totalRecords = 0;
            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q, // Không áp dụng điều kiện Where
                query.gridRequest,
                ref totalRecords
            );
            var data = dataQuery.AsEnumerable()
                .Select((m, i) => new UserDto
                {
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    Id = m.Id,
                    StoreId = m.StoreId,
                    FullName = m.FullName,
                    Email = m.Email,
                    UserType = m.UserType,
                    ModerationStatus = m.ModerationStatus,
                    Created = m.Created,
                    LastModified = m.LastModified,
                    CreatedBy = m.CreatedBy,
                    LastModifiedBy = m.LastModifiedBy
                });

            DataTableJson dataTableJson = new DataTableJson(data, query.draw, totalRecords);
            dataTableJson.querytext = dataQuery.ToString();
            return dataTableJson;
        }
        #endregion

        #region Thêm, Sửa, Xóa
        public async Task<Guid> CreateAsync(UserDto item)
        {
            try
            {
                // Kiểm tra email đã tồn tại
                var users = await _unitOfWork.UserRepo.GetAllAsync();
                if (users.Any(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant()))
                {
                    throw new Exception("Email đã tồn tại");
                }

                User user = new User
                {
                    StoreId = item.StoreId,
                    FullName = item.FullName,
                    Email = item.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(item.Password), // Mã hóa mật khẩu
                    UserType = item.UserType
                };
                user.FillDataForInsert(Guid.NewGuid()); // Giả định CreatedBy là một Guid ngẫu nhiên
                _unitOfWork.UserRepo.Add(user);
                await _unitOfWork.CompleteAsync();
                return user.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, UserDto item)
        {
            try
            {
                var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy người dùng");
                }

                user.StoreId = item.StoreId;
                user.FullName = item.FullName;
                user.Email = item.Email;
                user.Password = BCrypt.Net.BCrypt.HashPassword(item.Password); // Mã hóa mật khẩu
                user.UserType = item.UserType;
                user.FillDataForUpdate(Guid.NewGuid()); // Giả định LastModifiedBy là một Guid ngẫu nhiên
                _unitOfWork.UserRepo.Update(user);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng");
            }
            _unitOfWork.UserRepo.Delete(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion

        #region Đăng nhập
        #region Đăng nhập
        public async Task<User?> LoginAsync(LoginDto item)
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant());
            if (user != null && BCrypt.Net.BCrypt.Verify(item.Password, user.Password))
            {
                return user;
            }
            return null;
        }
        #endregion
        #endregion
    }
}