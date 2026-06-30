using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Task_ProjectManagementAPI.Application.Validators;

namespace Task_ProjectManagementAPI.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();
            return services;
        }
    }
}
