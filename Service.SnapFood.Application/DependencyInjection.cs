using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Application.Interfaces.Jwt;
using Service.SnapFood.Application.Service;
using Service.SnapFood.Application.Service.Jwt;
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

            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IJwtService, JwtService>();

            return services;
        }
    }
}
