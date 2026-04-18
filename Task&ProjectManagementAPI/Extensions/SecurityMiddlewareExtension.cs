namespace Task_ProjectManagementAPI.Extensions
{
    public static class SecurityMiddlewareExtension
    {
        public static IApplicationBuilder UseSecutiry(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
