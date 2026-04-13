using FluentValidation;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid project ID.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MinimumLength(3).WithMessage("Project name must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Project name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
