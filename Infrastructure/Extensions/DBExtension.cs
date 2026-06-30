using Application.Services.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task_ProjectManagementAPI.Infrastructure.Data;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class DBExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("DbContext"), b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
          
            services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
