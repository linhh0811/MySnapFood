using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Interfaces.Jwt;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System.Text.RegularExpressions;

namespace Service.SnapFood.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IJwtService jwtService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _emailService = emailService;
        }


        #region Lấy dữ liệu
        public async Task<List<User>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            return users.ToList();
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");

            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Xử lý CreatedBy
            string createdByName = "Hệ thống";
            if (user.CreatedBy != Guid.Empty)
            {
                var createdByUser = await _unitOfWork.UserRepo.GetByIdAsync(user.CreatedBy);
                createdByName = createdByUser?.FullName ?? "Không xác định";
            }

            // Xử lý LastModifiedBy
            string lastModifiedByName = "Hệ thống";
            if (user.LastModifiedBy != Guid.Empty)
            {
                var lastModifiedByUser = await _unitOfWork.UserRepo.GetByIdAsync(user.LastModifiedBy);
                lastModifiedByName = lastModifiedByUser?.FullName ?? "Không xác định";
            }

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Numberphone = user.Numberphone,
                UserType = user.UserType,
                ModerationStatus = user.ModerationStatus,
                Created = user.Created,
                LastModified = user.LastModified,
                CreatedBy = user.CreatedBy,
                LastModifiedBy = user.LastModifiedBy,
                CreatedByName = createdByName,
                LastModifiedByName = lastModifiedByName
            };
        }




        public DataTableJson GetPaged(BaseQuery query)
        {
            if (query == null || query.gridRequest == null)
                throw new ArgumentNullException(nameof(query), "Thông tin phân trang không hợp lệ");

            int totalRecords = 0;
            var dataQuery = _unitOfWork.UserRepo.FilterData(
                q => q.Include(x => x.Orderes)
                .Where(u => u.UserType == UserType.User), // Lọc chỉ lấy UserType.User
                query.gridRequest,
                ref totalRecords
            );
            var data = dataQuery.AsEnumerable()
                .OrderByDescending(m => m.Orderes.Count())
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
                    LastModifiedBy = m.LastModifiedBy,
                    TongDonHang = m.Orderes.Count(),
                    DonHangBiHuy = m.Orderes.Count(x => x.Status == StatusOrder.Cancelled)

                });

            return new DataTableJson(data, query.draw, totalRecords);
        }
        #endregion

        #region Sửa


        public async Task<bool> UpdateAsync(Guid id, UserDto item)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID không hợp lệ");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Dữ liệu cập nhật không được để trống");

            // Validate input
            ValidateUserInput(item);



            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");

            // Kiểm tra email trùng
            if (user.Email.ToLower() != item.Email.ToLower())
            {
                var existingUsers = _unitOfWork.UserRepo.FindWhere(x => x.UserType == UserType.User);
                if (existingUsers.Any(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant()))
                    throw new Exception("Email đã tồn tại");
            }

            if (item.IsThayDoiMatKhau)
            {
                if (!BCrypt.Net.BCrypt.Verify(item.Password.Trim(), user.Password))
                {
                    throw new Exception("Mật khẩu không chính xác");
                }
                if (item.PasswordMoi != item.PasswordConfirmMoi)
                {
                    throw new Exception("Mật khẩu xác nhận không chính xác");
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(item.PasswordMoi.Trim());

            }

            user.FullName = item.FullName;
            user.Email = item.Email.ToLower();
            user.Numberphone = item.Numberphone;

            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.CompleteAsync();

            // Gửi email thông báo cập nhật
            string subject = "Cập nhật thông tin tài khoản";

            string body = string.Empty;
            if (item.IsThayDoiMatKhau)
            {
                body = $@"
                    <p>Xin chào <strong>{item.FullName}</strong>,</p>
                    <p>Thông tin tài khoản của bạn đã được cập nhật.</p>

                    <h4>📋 Thông tin mới:</h4>
                    <ul>
                        <li><strong>Họ tên:</strong> {item.FullName}</li>
                        <li><strong>Email:</strong> {item.Email}</li>
                        <li><strong>SĐT:</strong> {item.Numberphone}</li>
                        <li><strong>Mật khẩu:</strong> Đã được thay đổi</li>

                    </ul>
                    <p>Trân trọng,<br>Hệ thống quản lý</p>
                    <p>-----------------------------------------------------------------</p>
                ";
            }
            else
            {
                body = $@"
                    <p>Xin chào <strong>{item.FullName}</strong>,</p>
                    <p>Thông tin tài khoản của bạn đã được cập nhật.</p>

                    <h4>📋 Thông tin mới:</h4>
                    <ul>
                        <li><strong>Họ tên:</strong> {item.FullName}</li>
                        <li><strong>Email:</strong> {item.Email}</li>
                        <li><strong>SĐT:</strong> {item.Numberphone}</li>
                    </ul>
                    <p>Trân trọng,<br>Hệ thống quản lý</p>
                    <p>-----------------------------------------------------------------</p>
                ";
            }


            await _emailService.SendEmailAsync(item.Email, subject, body);

            return true;
        }

        #endregion

        #region Đăng nhập, đăng ký
        public async Task<AuthResponseDto?> LoginAsync(LoginDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Thông tin đăng nhập không được để trống");
            if (string.IsNullOrWhiteSpace(item.Email) || string.IsNullOrWhiteSpace(item.Password))
                throw new ArgumentException("Email và mật khẩu không được để trống");

            if (!IsValidEmail(item.Email))
                throw new ArgumentException("Email không hợp lệ");
            var loweredEmail = item.Email.ToLowerInvariant().Trim();
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            var user = users.Where(x => x.UserType == UserType.User).FirstOrDefault(u => u.Email == loweredEmail);
            if (user == null || !BCrypt.Net.BCrypt.Verify(item.Password, user.Password))
                return null;
            if (user.ModerationStatus != ModerationStatus.Approved)
            {
                throw new Exception("Tài khoản của bạn đã bị khóa bởi hệ thống");
            }
            AuthDto authDto = new AuthDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
            };

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(authDto)
            };
        }



        public async Task<Guid> RegisterAsync(RegisterDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Thông tin đăng ký không được để trống");


            ValidateRegisterInput(item);


            var users = await _unitOfWork.UserRepo.GetAllAsync();
            if (users.Where(x => x.UserType == UserType.User).Any(u => u.Email.ToLowerInvariant().Trim() == item.Email.ToLowerInvariant().Trim()))
                throw new Exception("Email đã tồn tại");
            var loweredEmail = item.Email.ToLowerInvariant().Trim();

            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FullName = item.FullName.ToLower(),
                Email = loweredEmail,
                Password = BCrypt.Net.BCrypt.HashPassword(item.Password),
                UserType = UserType.User
            };

            _unitOfWork.UserRepo.Add(user);


            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,

            };
            _unitOfWork.CartRepo.Add(cart);


            await _unitOfWork.CompleteAsync();

            return userId;
        }

        #endregion
        #region Duyệt, hủy duyệt
        private void ValidateUserInput(UserDto item)
        {
            if (string.IsNullOrWhiteSpace(item.FullName))
                throw new ArgumentException("Họ tên không được để trống");
            if (string.IsNullOrWhiteSpace(item.Email))
                throw new ArgumentException("Email không được để trống");
            if (!IsValidEmail(item.Email))
                throw new ArgumentException("Email không hợp lệ");

        }

        private void ValidateRegisterInput(RegisterDto item)
        {

            if (string.IsNullOrWhiteSpace(item.Email))
                throw new ArgumentException("Email không được để trống");
            if (!IsValidEmail(item.Email.ToLower()))
                throw new ArgumentException("Email không hợp lệ");
            if (string.IsNullOrWhiteSpace(item.Password))
                throw new ArgumentException("Mật khẩu không được để trống");

        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        #endregion

        #region Otp
        public async Task SendOtpDangKy(OtpConfirmDto otpConfirmDto)
        {
            var loweredEmail = otpConfirmDto.Email.ToLowerInvariant();
            var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Email.ToLower() == otpConfirmDto.Email.ToLower() && x.UserType == UserType.User);
            if (user is not null)
            {
                throw new Exception("Email đã tồn tại trong hệ thống");
            }
            // Kiểm tra gửi liên tục dựa trên Email (không phụ thuộc user tồn tại)
            var otpCheck = _unitOfWork.OtpConfirmRepository.FirstOrDefault(x =>
                x.Email == loweredEmail &&
                x.Created >= DateTime.Now.AddMinutes(-1)
            );

            if (otpCheck is not null)
            {
                throw new Exception("Không thể gửi liên tục, vui lòng đợi");
            }

            string fullName = user?.FullName ?? "Khách hàng";

            OtpConfirm otpConfirm = new OtpConfirm()
            {
                UserId = user?.Id ?? Guid.Empty,
                Email = loweredEmail,
                OtpCode = GetRandom6DigitString()
            };

            _unitOfWork.OtpConfirmRepository.Add(otpConfirm);
            await _unitOfWork.CompleteAsync();

            string subject = user != null ? "Lấy lại mật khẩu Snap-Food" : "Xác thực đăng ký Snap-Food";

            string body = $@"
                <p>Xin chào <strong>{fullName}</strong>,</p>
                <p>{(user != null ? "Yêu cầu lấy lại mật khẩu" : "Yêu cầu xác thực đăng ký tài khoản")}</p>

                <h4>📋 Mã xác nhận:</h4>
                <ul>
                    <li><strong>Mã xác nhận của bạn là:</strong> {otpConfirm.OtpCode}</li>
                    <li>Mã xác nhận chỉ tồn tại trong <strong>1 phút</strong></li>
                </ul>
                <p>Vui lòng không cung cấp mã xác nhận cho người lạ.</p>
                <p>Trân trọng,<br>Hệ thống quản lý</p>
                <p>-----------------------------------------------------------------</p>                   
            ";

            await _emailService.SendEmailAsync(loweredEmail, subject, body);
           

        }
        public async Task SendOtp(OtpConfirmDto otpConfirmDto)
        {
            var loweredEmail = otpConfirmDto.Email.ToLowerInvariant();
            var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Email.ToLower() == otpConfirmDto.Email.ToLower()&&x.UserType == UserType.User);
            if (user is not null)
            {
                // Kiểm tra gửi liên tục dựa trên Email (không phụ thuộc user tồn tại)
                var otpCheck = _unitOfWork.OtpConfirmRepository.FirstOrDefault(x =>
                    x.Email == loweredEmail &&
                    x.Created >= DateTime.Now.AddMinutes(-1)
                );

                if (otpCheck is not null)
                {
                    throw new Exception("Không thể gửi liên tục, vui lòng đợi");
                }

                string fullName = user?.FullName ?? "Khách hàng";

                OtpConfirm otpConfirm = new OtpConfirm()
                {
                    UserId = user?.Id ?? Guid.Empty,
                    Email = loweredEmail,
                    OtpCode = GetRandom6DigitString()
                };

                _unitOfWork.OtpConfirmRepository.Add(otpConfirm);
                await _unitOfWork.CompleteAsync();

                string subject = user != null ? "Lấy lại mật khẩu Snap-Food" : "Xác thực đăng ký Snap-Food";

                string body = $@"
                    <p>Xin chào <strong>{fullName}</strong>,</p>
                    <p>{(user != null ? "Yêu cầu lấy lại mật khẩu" : "Yêu cầu xác thực đăng ký tài khoản")}</p>

                    <h4>📋 Mã xác nhận:</h4>
                    <ul>
                        <li><strong>Mã xác nhận của bạn là:</strong> {otpConfirm.OtpCode}</li>
                        <li>Mã xác nhận chỉ tồn tại trong <strong>1 phút</strong></li>
                    </ul>
                    <p>Vui lòng không cung cấp mã xác nhận cho người lạ.</p>
                    <p>Trân trọng,<br>Hệ thống quản lý</p>
                    <p>-----------------------------------------------------------------</p>                   
                ";

                await _emailService.SendEmailAsync(loweredEmail, subject, body);
            }
            else
            {
                throw new Exception("Email không tồn tại");
            }
            
        }

        public async Task SendOtpStaff(OtpConfirmDto otpConfirmDto)
        {
            var loweredEmail = otpConfirmDto.Email.ToLowerInvariant();
            var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Email.ToLower() == otpConfirmDto.Email.ToLower() && x.UserType == UserType.Store);
            // Kiểm tra gửi liên tục dựa trên Email (không phụ thuộc user tồn tại)
            if (user is not null)
            {
                // Kiểm tra gửi liên tục dựa trên Email (không phụ thuộc user tồn tại)
                var otpCheck = _unitOfWork.OtpConfirmRepository.FirstOrDefault(x =>
                    x.Email == loweredEmail &&
                    x.Created >= DateTime.Now.AddMinutes(-1)
                );

                if (otpCheck is not null)
                {
                    throw new Exception("Không thể gửi liên tục, vui lòng đợi");
                }

                string fullName = user?.FullName ?? "Khách hàng";

                OtpConfirm otpConfirm = new OtpConfirm()
                {
                    UserId = user?.Id ?? Guid.Empty,
                    Email = loweredEmail,
                    OtpCode = GetRandom6DigitString()
                };

                _unitOfWork.OtpConfirmRepository.Add(otpConfirm);
                await _unitOfWork.CompleteAsync();

                string subject = user != null ? "Lấy lại mật khẩu Snap-Food" : "Xác thực đăng ký Snap-Food";

                string body = $@"
                    <p>Xin chào <strong>{fullName}</strong>,</p>
                    <p>{(user != null ? "Yêu cầu lấy lại mật khẩu" : "Yêu cầu xác thực đăng ký tài khoản")}</p>

                    <h4>📋 Mã xác nhận:</h4>
                    <ul>
                        <li><strong>Mã xác nhận của bạn là:</strong> {otpConfirm.OtpCode}</li>
                        <li>Mã xác nhận chỉ tồn tại trong <strong>1 phút</strong></li>
                    </ul>
                    <p>Vui lòng không cung cấp mã xác nhận cho người lạ.</p>
                    <p>Trân trọng,<br>Hệ thống quản lý</p>
                    <p>-----------------------------------------------------------------</p>                   
                ";

                await _emailService.SendEmailAsync(loweredEmail, subject, body);
            }
            else
            {
                throw new Exception("Email không tồn tại");
            }
        }

        public async Task VerifyOtp(OtpConfirmDto item)
        {
            if (item == null || string.IsNullOrEmpty(item.Email) || string.IsNullOrEmpty(item.OtpCode))
                throw new ArgumentException("Thông tin không hợp lệ");

            var loweredEmail = item.Email.ToLowerInvariant();

            var otpCheck = _unitOfWork.OtpConfirmRepository.FirstOrDefault(x =>
                x.Email == loweredEmail &&
                x.OtpCode == item.OtpCode.Trim() &&
                x.Created >= DateTime.Now.AddMinutes(-1)
            );

            if (otpCheck == null)
                throw new Exception("Mã OTP không chính xác hoặc đã hết hạn");

            if (otpCheck.UserId == Guid.Empty)
            {
                // Registration
                var users = await _unitOfWork.UserRepo.GetAllAsync();
                if (users.Where(x => x.UserType == UserType.User).Any(u => u.Email.ToLowerInvariant() == loweredEmail))
                    throw new Exception("Email đã tồn tại");
            }
            else
            {
                // Forgot password
                var user = await _unitOfWork.UserRepo.GetByIdAsync(otpCheck.UserId);
                if (user == null)
                    throw new Exception("Không tìm thấy người dùng");
            }

            // Delete OTP
            _unitOfWork.OtpConfirmRepository.Delete(otpCheck);
            await _unitOfWork.CompleteAsync();
        }




        public async Task LayLaiMatKhau(OtpConfirmDto otpConfirmDto)
        {
            // Tìm OTP dựa trên Email
            var loweredEmail = otpConfirmDto.Email.ToLowerInvariant();

            var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Email == loweredEmail);
            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại");
            }

            if (otpConfirmDto.PasswordMoi != otpConfirmDto.PasswordConfirmMoi)
            {
                throw new Exception("Mật khẩu xác nhận không chính xác");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(otpConfirmDto.PasswordMoi.Trim());
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        public string GetRandom6DigitString()
        {
            Random random = new Random();
            int number = random.Next(0, 1000000); // từ 0 đến 999999
            return number.ToString("D6"); // định dạng để luôn có 6 chữ số, thêm số 0 phía trước nếu cần
        }


        #endregion

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
    }
}