using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Interfaces.Jwt;
using Service.SnapFood.Application.Service;
using Service.SnapFood.Application.Service.Jwt;
using Service.SnapFood.Share.Interface.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IComboService, ComboService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IRequestContext, RequestContext>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IMomoService, MomoService>();
            services.AddScoped<IThongTinGiaoHangService, ThongTinGiaoHangService>();


            services.AddScoped<IDiscountCodeService, DiscountCodeService>();
            return services;
        }
    }
}
