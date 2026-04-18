using Microsoft.EntityFrameworkCore;
using Test_Api.Data;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class DBExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("DbContext")));
            return services;
        }
    }
}
