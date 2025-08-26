using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Interfaces.Jwt;
using Service.SnapFood.Application.Service.Jwt;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

using System.Text.RegularExpressions;


namespace Service.SnapFood.Application.Service
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEmailService _emailService;

        private readonly IJwtService _jwtService;
        public StaffService(IUnitOfWork unitOfWork, IEmailService emailService, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _jwtService = jwtService;
        }
   


        #region Lấy dữ liệu
        public async Task<List<User>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            return users.ToList();
        }

        public async Task<StaffDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            var allRoles = _unitOfWork.RolesRepo.GetAll();
            var userRoles = _unitOfWork.UserRoleRepo.GetAll()
                            .Where(ur => ur.UserId == user.Id)
                            .ToList();

            var roleQuanLys = allRoles
                .Where(x => x.EnumRole == EnumRole.Admin || x.EnumRole == EnumRole.Manager)
                .ToList();
            StaffDto StaffDto = new StaffDto();

            StaffDto.Id = user.Id.ToString();
            StaffDto.FullName = user.FullName;
            StaffDto.Email = user.Email;
            StaffDto.Numberphone = user.Numberphone??string.Empty;
            StaffDto.IsHeThong = user.IsHeThong;

        // ✅ Danh sách tất cả EnumRole của user
            StaffDto.Role = (from ur in userRoles
                             join r in allRoles on ur.RoleId equals r.Id
                             select r.EnumRole).ToList();

            StaffDto.Created = user.Created;
            StaffDto.LastModified = user.LastModified;


            return StaffDto;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            if (query == null || query.gridRequest == null)
                throw new ArgumentNullException(nameof(query), "Thông tin phân trang không hợp lệ");

            int totalRecords = 0;
            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q.Where(x => x.UserType == UserType.Store),
                query.gridRequest,
                ref totalRecords

            );
            var allRoles = _unitOfWork.RolesRepo.GetAll();
            var roleQuanLys = allRoles
                .Where(x => x.EnumRole == EnumRole.Admin || x.EnumRole == EnumRole.Manager)
                .ToList();
            var userRoles = _unitOfWork.UserRoleRepo.GetAll();
            var data = dataQuery.AsEnumerable()
                .Select((m, i) => new StaffDto
                {
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    Id = m.Id.ToString(),
                    FullName = m.FullName,
                    Email = m.Email,
                    Numberphone = m.Numberphone ?? string.Empty,
                    ModerationStatus = m.ModerationStatus,
                    IsHeThong=m.IsHeThong,
                    Role = (from ur in userRoles
                            join r in allRoles on ur.RoleId equals r.Id
                            where ur.UserId == m.Id
                            select r.EnumRole).ToList()
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

            var users =  _unitOfWork.UserRepo.FindWhere(x=>x.UserType== UserType.Store);
            if (users.Any(u => u.Email.Trim().ToLower() == item.Email.Trim().ToLower()))
                throw new Exception("Email đã tồn tại");

            if (users.Any(u => u.Numberphone?.Trim().ToLower() == item.Numberphone?.Trim().ToLower()))
                throw new Exception("Số điện thoại đã tồn tại");

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

            
                var existingUsers = _unitOfWork.UserRepo.FindWhere(x=>x.UserType== UserType.Store);
                if (existingUsers.Any(u => u.Email.Trim().ToLower() == item.Email.Trim().ToLower()&&u.Id!=user.Id))
                    throw new Exception("Email đã tồn tại");
                if (existingUsers.Any(u => u.Numberphone?.Trim().ToLower() == item.Numberphone?.Trim().ToLower() && u.Id != user.Id))
                    throw new Exception("Số điện thoại đã tồn tại");
   



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
            if (!IsValidEmail(item.Email.Trim()))
                throw new ArgumentException("Email không hợp lệ");
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
        public AuthResponseDto? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepo.FirstOrDefault(u => u.Email == loginDto.Email                      
                        && u.UserType == UserType.Store);
            if (user is not null)
            {
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    return null;
                }
                if (user.ModerationStatus != ModerationStatus.Approved)
                {
                    return null;
                }
                AuthDto authDto = new AuthDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                };
                var roleIds = _unitOfWork.UserRoleRepo.FindWhere(ur => ur.UserId == user.Id)
                    .Select(ur => ur.RoleId).ToList();
                var roles = _unitOfWork.RolesRepo
                   .FindWhere(r => roleIds.Contains(r.Id)) 
                   .ToList();
                authDto.Roles = roles.Select(role => new AuthRoleDto
                {
                    EnumRole = role.EnumRole,
                    RoleName = role.RoleName
                }).ToList();
                return new AuthResponseDto
                {
                    Token = _jwtService.GenerateToken(authDto)
                };
            }
            else
            {
                return null;
            }
               

        }
    }
}