using BCrypt.Net;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Interfaces.Jwt;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

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

        //public async Task<User> GetByIdAsync(Guid id)
        //{
        //    if (id == Guid.Empty)
        //        throw new ArgumentException("ID không hợp lệ");

        //    var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
        //    if (user == null)
        //        throw new Exception("Không tìm thấy người dùng");

        //    return user;
        //}
        //public async Task<UserDto> GetByIdAsync(Guid id)
        //{
        //    if (id == Guid.Empty)
        //        throw new ArgumentException("ID không hợp lệ");

        //    var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
        //    if (user == null)
        //        throw new Exception("Không tìm thấy người dùng");

        //    // Lấy người tạo và người sửa cuối (nếu có)
        //    var createdByUser = await _unitOfWork.UserRepo.GetByIdAsync(user.CreatedBy);
        //    var lastModifiedByUser = await _unitOfWork.UserRepo.GetByIdAsync(user.LastModifiedBy);

        //    return new UserDto
        //    {
        //        Id = user.Id,
        //        FullName = user.FullName,
        //        Email = user.Email,
        //        UserType = user.UserType,
        //        ModerationStatus = user.ModerationStatus,
        //        Created = user.Created,
        //        LastModified = user.LastModified,
        //        CreatedBy = user.CreatedBy,
        //        LastModifiedBy = user.LastModifiedBy,
        //        CreatedByName = createdByUser?.FullName ?? "Không xác định",
        //        LastModifiedByName = lastModifiedByUser?.FullName ?? "Không xác định"

        //    };
        //}

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
                q => q.Where(u => u.UserType == UserType.User), // Lọc chỉ lấy UserType.User
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
            if (user.Email.ToLowerInvariant() != item.Email.ToLowerInvariant())
            {
                var existingUsers = await _unitOfWork.UserRepo.GetAllAsync();
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
            user.Email = item.Email;
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
                    <p>
                        <img src='https://iili.io/FbbYFJR.jpg' alt='Logo' width='285px' height='195px'/>
                    </p>
                    <p>
                        <h3><strong>SnapFood - Hệ thống quản lý cửa hàng</strong></h3>
                        <strong>Địa chỉ:</strong> 13, Trịnh Văn Bô, Nam Từ Liêm, Hà Nội <br>
                        <strong>Mobile | Zalo:</strong> +84(0) 98 954 7555 <br>
                        <strong>Email:</strong> snapfoodvn@gmail.com | snapfoodadmin03@gmail.com
                    </p>
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
                    <p>
                        <img src='https://iili.io/FbbYFJR.jpg' alt='Logo' width='285px' height='195px'/>
                    </p>
                    <p>
                        <h3><strong>SnapFood - Hệ thống quản lý cửa hàng</strong></h3>
                        <strong>Địa chỉ:</strong> 13, Trịnh Văn Bô, Nam Từ Liêm, Hà Nội <br>
                        <strong>Mobile | Zalo:</strong> +84(0) 98 954 7555 <br>
                        <strong>Email:</strong> snapfoodvn@gmail.com | snapfoodadmin03@gmail.com
                    </p>
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

            var users = await _unitOfWork.UserRepo.GetAllAsync();
            var user = users.Where(x=>x.UserType==UserType.User).FirstOrDefault(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant());
            if (user == null || !BCrypt.Net.BCrypt.Verify(item.Password, user.Password))
                return null;
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
            if (users.Where(x=>x.UserType==UserType.User).Any(u => u.Email.ToLowerInvariant() == item.Email.ToLowerInvariant()))
                throw new Exception("Email đã tồn tại");


            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FullName = item.FullName.ToLower(),
                Email = item.Email.ToLower(),
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
        public async Task SendOtp(OtpConfirmDto otpConfirmDto)
        {
            var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Email == otpConfirmDto.Email.ToLower());
            if (user is null)
            {
                throw new Exception("Email không tồn tại trong hệ thống");
            }

            var otpCheck = _unitOfWork.OtpConfirmRepository.FirstOrDefault(x => (x.UserId == user.Id) && (x.Created >= DateTime.Now.AddMinutes(-1)));
            if (otpCheck is not null)
            {
                throw new Exception("Không thể gửi liên tục, vui lòng đợi");
            }
            OtpConfirm otpConfirm = new OtpConfirm()
            {
                UserId = user.Id,
                OtpCode = GetRandom6DigitString()
            };

            _unitOfWork.OtpConfirmRepository.Add(otpConfirm);
            await _unitOfWork.CompleteAsync();

            string subject = "Lấy lại mật khẩu Snap-Food";

            string body = $@"
                    <p>Xin chào <strong>{user.FullName}</strong>,</p>
                    <p>Thông tin tài khoản của bạn đã được cập nhật.</p>

                    <h4>📋 Lấy lại mật khẩu Snap-Food:</h4>
                    <ul>
                        <li><strong>Mã xác nhận của bạn là:</strong> {otpConfirm.OtpCode}</li>
                        <li>Mã xác nhận chỉ tồn tại trong <strong>1 phút</strong></li>
                       

                    </ul>
                    <p>Vui lòng không cung cấp mã xác nhận cho người lạ.</p>
                    <p>Trân trọng,<br>Hệ thống quản lý</p>
                    <p>-----------------------------------------------------------------</p>
                    <p>
                        <img src='https://iili.io/FbbYFJR.jpg' alt='Logo' width='285px' height='195px'/>
                    </p>
                    <p>
                        <h3><strong>SnapFood - Hệ thống quản lý cửa hàng</strong></h3>
                        <strong>Địa chỉ:</strong> 13, Trịnh Văn Bô, Nam Từ Liêm, Hà Nội <br>
                        <strong>Mobile | Zalo:</strong> +84(0) 98 954 7555 <br>
                        <strong>Email:</strong> snapfoodvn@gmail.com | snapfoodadmin03@gmail.com
                    </p>
                ";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }


        

        public async Task LayLaiMatKhau(OtpConfirmDto otpConfirmDto)
        {
            var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Email == otpConfirmDto.Email.ToLower());
            if (user is null)
            {
                throw new Exception("Email không tồn tại trong hệ thống");
            }

            var otpCheck = _unitOfWork.OtpConfirmRepository.FirstOrDefault(x => (x.UserId == user.Id) && (x.OtpCode== otpConfirmDto.OtpCode.Trim()) && (x.Created >= DateTime.Now.AddMinutes(-1)));
            if (otpCheck is null)
            {
                throw new Exception("Mã xác nhận không chính xác");
            }
            if (otpConfirmDto.PasswordMoi != otpConfirmDto.PasswordConfirmMoi)
            {
                throw new Exception("Mật khẩu xác nhận không chính xác");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(otpConfirmDto.PasswordMoi.Trim());
            _unitOfWork.UserRepo.Update(user);
            await _unitOfWork.CompleteAsync();

        }
        public  string GetRandom6DigitString()
        {
            Random random = new Random();
            int number = random.Next(0, 1000000); // từ 0 đến 999999
            return number.ToString("D6"); // định dạng để luôn có 6 chữ số, thêm số 0 phía trước nếu cần
        }

      
        #endregion
    }
}