using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Infrastructure.Repositorys.Base;
using Service.SnapFood.Infrastructure.Service;
using Service.SnapFood.Share.Interface.Extentions;

namespace Service.SnapFood.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                //options.EnableSensitiveDataLogging();
            });



            services.AddScoped<IDbContextTransaction>(provider => null!);


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHttpClient<ICallServiceRegistry, CallServiceRegistry>();



            return services;
        }
    }
}
