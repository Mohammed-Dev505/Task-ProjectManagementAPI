using Microsoft.OpenApi.Models;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task & Project Management API",
                    Version = "v1",
                    Description = "لوحة التحكم واختبار خدمات نظام ادارة المهام والمشاريع المؤمن ب JWT"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "ضع ال Token الخاص بك مباشرة في الحقل(بدون كتابة كلمة Bearer , ال Swagger سيكتبها تلقائياً)",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                       Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
