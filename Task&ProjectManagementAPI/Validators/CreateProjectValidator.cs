using FluentValidation;
using Test_Api.DTOs;
using System.Text.RegularExpressions;

namespace Task_ProjectManagementAPI.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Project name is required")
                .Must(name => name != null && name.Trim().Length >= 3)
                    .WithMessage("Project name must be at least 3 characters")
                .Must(name => name != null && name.Trim().Length <= 150)
                    .WithMessage("Project name cannot exceed 150 characters")
                .Must(name => name != null && Regex.IsMatch(name.Trim(), "[A-Za-z0-9]"))
                    .WithMessage("Project name must contain at least one letter or number")
                .WithName("Project Name");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
                .When(dto => !string.IsNullOrWhiteSpace(dto.Description));
        }
    }
}
