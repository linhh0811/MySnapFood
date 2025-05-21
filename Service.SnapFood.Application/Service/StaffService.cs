using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System.Text.RegularExpressions;

namespace Service.SnapFood.Application.Service
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Lấy dữ liệu
        public async Task<List<User>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            return users.ToList();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            return user;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            if (query == null || query.gridRequest == null)
                throw new ArgumentNullException(nameof(query), "Thông tin phân trang không hợp lệ");

            int totalRecords = 0;
            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q,
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

            return new DataTableJson(data, query.draw, totalRecords);
        }
        #endregion

        #region Thêm, sửa, xóa
        public async Task<Guid> CreateAsync(UserDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu thêm mới không được để trống");

            ValidateStaffInput(item);

            var users = await _unitOfWork.UserRepo.GetAllAsync();
            if (users.Any(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant()))
                throw new Exception("Email đã tồn tại");

            var user = new User
            {
                StoreId = item.StoreId,
                FullName = item.FullName,
                Email = item.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(item.Password),
                UserType = item.UserType
            };
            user.FillDataForInsert(Guid.NewGuid());

            _unitOfWork.UserRepo.Add(user);
            await _unitOfWork.CompleteAsync();
            return user.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, UserDto item)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu cập nhật không được để trống");

            ValidateStaffInput(item);

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            if (user.Email.ToLowerInvariant() != item.Email.ToLowerInvariant())
            {
                var existingUsers = await _unitOfWork.UserRepo.GetAllAsync();
                if (existingUsers.Any(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant()))
                    throw new Exception("Email đã tồn tại");
            }

            user.StoreId = item.StoreId;
            user.FullName = item.FullName;
            user.Email = item.Email;
            if (!string.IsNullOrWhiteSpace(item.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            user.UserType = item.UserType;
            user.FillDataForUpdate(Guid.NewGuid());

            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            _unitOfWork.UserRepo.Delete(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion

        #region Duyệt, hủy duyệt
        public async Task<bool> ApproveAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                return false;

            user.ModerationStatus = ModerationStatus.Approved;
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RejectAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                return false;

            user.ModerationStatus = ModerationStatus.Rejected;
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion

        #region Validate
        private void ValidateStaffInput(UserDto item)
        {
            if (string.IsNullOrWhiteSpace(item.FullName))
                throw new ArgumentException("Họ tên không được để trống");
            if (string.IsNullOrWhiteSpace(item.Email))
                throw new ArgumentException("Email không được để trống");
            if (!IsValidEmail(item.Email))
                throw new ArgumentException("Email không hợp lệ");
            if (string.IsNullOrWhiteSpace(item.Password))
                throw new ArgumentException("Mật khẩu không được để trống");
            if (item.Password.Length < 6)
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự");
            if (!IsStrongPassword(item.Password))
                throw new ArgumentException("Mật khẩu phải chứa ít nhất một chữ cái in hoa, một chữ cái thường và một số");
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsStrongPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
        }
        #endregion
    }
}