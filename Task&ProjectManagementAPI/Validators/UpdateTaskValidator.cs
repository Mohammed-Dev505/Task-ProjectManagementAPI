using FluentValidation;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Validators
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskValidator()
        {
            RuleFor(i => i.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Invalid task ID.");

            RuleFor(t => t.Title)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Task title is required.")
                .Must(t => !string.IsNullOrWhiteSpace(t)).WithMessage("Task title is required.")
                .Must(t => t != null && t.Trim().Length >= 3).WithMessage("Task title must be at least 3 characters.")
                .Must(t => t != null && t.Trim().Length <= 100).WithMessage("Task title cannot exceed 100 characters.");

            RuleFor(d => d.Description)
                .Must(desc => string.IsNullOrWhiteSpace(desc) || desc.Trim().Length <= 1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(d => d.DueDate)
                .Must(due => !due.HasValue || due.Value > DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");

            RuleFor(p => p.Priority).IsInEnum().WithMessage("Invalid priority value.");
        }
    }
}