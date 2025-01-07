using FluentValidation;

namespace Employee.Management.Application.DTOs.Employee.Validators
{
    public class DeleteEmployeeDtoValidator : AbstractValidator<DeleteEmployeeDto>
    {
        public DeleteEmployeeDtoValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(p => p.EmployeeId).NotNull().NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
