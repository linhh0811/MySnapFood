using Microsoft.AspNetCore.Components.Forms;

namespace Service.SnapFood.Manage.Infrastructure.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _sharedImagePath;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
            // Điều chỉnh đường dẫn để trỏ đến thư mục Images trong Service.SnapFood.Share
            _sharedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Service.SnapFood.Share", "Images");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(_sharedImagePath))
            {
                Directory.CreateDirectory(_sharedImagePath);
            }
        }

        public async Task<string> SaveImageAsync(IBrowserFile file)
        {
            // Kiểm tra file hợp lệ
            if (file == null || file.Size == 0)
            {
                throw new ArgumentException("File is invalid or empty.");
            }

            // Tạo tên file duy nhất
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
            var filePath = Path.Combine(_sharedImagePath, fileName);

            // Lưu file
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(stream);

            return fileName; // Trả về tên file để sử dụng sau này
        }

        public string GetImageUrl(string fileName)
        {
            // Trả về đường dẫn tương đối để truy cập ảnh từ Service.SnapFood.Share
            return $"/Images/{fileName}";
        }
    }
}
