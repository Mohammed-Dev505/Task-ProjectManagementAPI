using Microsoft.AspNetCore.Identity;
using Test_Api.Data;
using Test_Api.Data.Models;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}
