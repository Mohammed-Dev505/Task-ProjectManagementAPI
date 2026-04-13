using System;
using FluentValidation;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Validators
{
    public class UpdateTaskStatusValidator : AbstractValidator<UpdateTaskStatusDto>
    {
        public UpdateTaskStatusValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Request body is required.");

            RuleFor(i => i.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Invalid task ID. ID must be greater than zero.");

            RuleFor(s => s.Status)
                .Cascade(CascadeMode.Stop)
                .Must(status => Enum.IsDefined(typeof(TaskStatus), status))
                .WithMessage("Invalid status value.");
        }
    }
}
