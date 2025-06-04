using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System.Data;
using System.Text.RegularExpressions;


namespace Service.SnapFood.Application.Service
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public StaffService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
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

            ).Where(x => x.UserType == Domain.Enums.UserType.Store );

            var data = dataQuery.AsEnumerable()
                .Select((m, i) => new StaffDto
                {
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    Id = m.Id.ToString(),
                    FullName = m.FullName,
                    Email = m.Email,
                    Numberphone = m.Numberphone ?? string.Empty,
                    ModerationStatus = m.ModerationStatus
                });

            return new DataTableJson(data, query.draw, totalRecords);
        }
        #endregion

        #region Thêm, sửa, xóa
        public async Task<Guid> CreateAsync(StaffDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu thêm mới không được để trống");

            ValidateStaffInput(item);

            var users = await _unitOfWork.UserRepo.GetAllAsync();
            if (users.Any(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant()))
                throw new Exception("Email đã tồn tại");

            string password = GenerateRandomPassword();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                FullName = item.FullName,
                Email = item.Email,
                Numberphone = item.Numberphone,
                Password = hashedPassword,
                UserType = Domain.Enums.UserType.Store
            };
            user.FillDataForInsert(Guid.NewGuid());

            _unitOfWork.UserRepo.Add(user);
            await _unitOfWork.CompleteAsync();
            
            string subject = "Thông tin tài khoản nhân viên";
            string body = $@"
                <p>Xin chào <strong>{item.FullName}</strong>,</p>

                <h4>🔐 Thông tin đăng nhập:</h4>
                <ul>
                    <li><strong>Email:</strong> {item.Email}</li>
                    <li><strong>Mật khẩu:</strong> {password}</li>
                </ul>

                <p style='color: red;'><b>Lưu ý:</b> Vui lòng đổi mật khẩu sau khi đăng nhập.</p>
                <p>Trân trọng,<br>Hệ thống quản lý</p>
                <p>-----------------------------------------------------------------</p>
                <p>
                    <img src='https://i.imgur.com/llw3FXb.jpeg' alt='Logo' width='285px' height='165px'/>
                </p>
                <p>
                    <h3><strong>BeeFood - Hệ thống quản lý cửa hàng</strong></h3> <br>
                    <strong>Address:</strong> 13, Trinh Van Bo, Nam Tu Liem, Ha noi <br>
                    <strong>Mobile | Zalo:</strong> +84(0) 98 954 7555 <br>
                    <strong>Email:</strong> beefoodvn@gmail.com | beefoodadmin03@gmail.com
                </p>
            ";

            await _emailService.SendEmailAsync(item.Email, subject, body);

            return user.Id;
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            Random rnd = new Random();
            return new string(Enumerable.Repeat(valid, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public async Task<bool> UpdateAsync(Guid id, StaffDto item)
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

            user.FullName = item.FullName;
            user.Email = item.Email;
            user.Numberphone = item.Numberphone;
            user.FillDataForUpdate(Guid.NewGuid());

            string subject = "Cập nhật thông tin tài khoản";
            string body = $@"
                <p>Xin chào <strong>{item.FullName}</strong>,</p>
                <p>Thông tin tài khoản của bạn đã được cập nhật.</p>

                <h4>📋 Thông tin mới:</h4>
                <ul>
                    <li><strong>Họ tên:</strong> {item.FullName}</li>
                    <li><strong>Email:</strong> {item.Email}</li>
                    <li><strong>SĐT:</strong> {item.Numberphone}</li>
                </ul>

                <p>Nếu bạn không yêu cầu cập nhật này, vui lòng liên hệ quản trị viên.</p>
                <p>Trân trọng,<br>Hệ thống quản lý</p>
                <p>-----------------------------------------------------------------</p>
                <p>
                    <img src='https://i.imgur.com/llw3FXb.jpeg' alt='Logo' width='285px' height='165px'/>
                </p>
                <p>
                    <h3><strong>BeeFood - Hệ thống quản lý cửa hàng</strong></h3>
                    <strong>Address:</strong> 13, Trinh Van Bo, Nam Tu Liem, Ha noi <br>
                    <strong>Mobile | Zalo:</strong> +84(0) 98 954 7555 <br>
                    <strong>Email:</strong> beefoodvn@gmail.com | beefoodadmin03@gmail.com
                </p>
            ";
            await _emailService.SendEmailAsync(item.Email, subject, body);

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
        private void ValidateStaffInput(StaffDto item)
        {
            if (string.IsNullOrWhiteSpace(item.FullName))
                throw new ArgumentException("Họ tên không được để trống");
            if (string.IsNullOrWhiteSpace(item.Email))
                throw new ArgumentException("Email không được để trống");
            if (!IsValidEmail(item.Email))
                throw new ArgumentException("Email không hợp lệ");
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        
        #endregion
    }
}