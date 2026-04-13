using FluentValidation;
using FluentValidation.Validators;
using Task_ProjectManagementAPI.Data.Models;

namespace Task_ProjectManagementAPI.Validators
{
    public class TokenRequestModelValidator : AbstractValidator<TokenRequestModel>
    {
        public TokenRequestModelValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Please enter a valid email address.")
                .MaximumLength(256).WithMessage($"Email must not exceed 256 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage($"Password must be at least {8} characters long.")
                .MaximumLength(128).WithMessage($"Password must not exceed {128} characters.");
        }
    }
}
