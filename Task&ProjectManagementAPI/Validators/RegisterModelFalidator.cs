using FluentValidation;
using Task_ProjectManagementAPI.Data.Models;

namespace Task_ProjectManagementAPI.Validators
{
    // Renamed the class to fix the typo and improve clarity
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            // Stop validation on first failure for each rule set
            RuleFor(u => u.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters")
                .Matches("^\\S+$").WithMessage("Username cannot contain whitespace");

            RuleFor(e => e.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(p => p.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(128).WithMessage("Password cannot exceed 128 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("\\d").WithMessage("Password must contain at least one number")
                .Matches("[\\W_]").WithMessage("Password must contain at least one special character");
        }
    }
}
