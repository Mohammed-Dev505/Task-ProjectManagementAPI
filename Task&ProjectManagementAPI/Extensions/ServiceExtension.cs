using Task_ProjectManagementAPI.Middleware;
using Task_ProjectManagementAPI.Services.Implementations;
using Task_ProjectManagementAPI.Services.Interfaces;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<ExceptionMiddleware>();
            return services;
        }
    }
}
