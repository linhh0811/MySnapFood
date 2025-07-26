using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.EF.Contexts
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Cấu hình chuỗi kết nối, thay "Your_Connection_String_Here" bằng chuỗi kết nối của bạn
            optionsBuilder.UseSqlServer("Data Source=VietManhABC\\SQLEXPRESS;Initial Catalog=Do_An_Nhanh_API;Integrated Security=True;TrustServerCertificate=true");

            return new AppDbContext(optionsBuilder.Options);
        }

    }
}
