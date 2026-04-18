using Microsoft.AspNetCore.Builder;
using Task_ProjectManagementAPI.Middleware;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class MiddlewareExtension
    {
        public static  IApplicationBuilder UseGlobalException (this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
