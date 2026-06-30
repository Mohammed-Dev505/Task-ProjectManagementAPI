using FluentValidation;
using Task_ProjectManagementAPI.Application.DTOs;

namespace Task_ProjectManagementAPI.Application.Validators
{
    public class UpdateTaskStatusValidator : AbstractValidator<UpdateTaskStatusDto>
    {
        public UpdateTaskStatusValidator()
        {

            RuleFor(s => s.Status)
                .Cascade(CascadeMode.Stop)
                .Must(status => Enum.IsDefined(typeof(TaskStatus), status))
                .WithMessage("Invalid status value.");
        }
    }
}
