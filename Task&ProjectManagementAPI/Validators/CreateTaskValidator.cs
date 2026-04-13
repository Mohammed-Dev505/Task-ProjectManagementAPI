using FluentValidation;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskValidator()
        {
            // Stop on first failure for title rules to avoid multiple messages for the same problem
            RuleFor(t => t.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Task title is required.")
                .MinimumLength(3).WithMessage("Task title must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Task title cannot exceed 100 characters.");

            RuleFor(d => d.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.")
                .When(s => !string.IsNullOrWhiteSpace(s.Description));

            RuleFor(p => p.ProjectId)
                .GreaterThan(0)
                .WithMessage("Invalid project ID.");

            RuleFor(d => d.DueDate)
                .Must(due => !due.HasValue || due.Value > DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");

            RuleFor(p => p.Priority)
                .IsInEnum()
                .WithMessage("Invalid priority value.");
        }
    }
}
