using Employee.Management.Application.Contracts.Persistence;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Employee.Management.Application.DTOs.Employee.Validators
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator(IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
        {
            Include(new IEmployeeDtoValidator(departmentRepository, positionRepository));

            RuleFor(p => p.Email).NotNull().NotEmpty().WithMessage("{PropertyName} is required").EmailAddress().WithMessage("Invalid email format.");

            RuleFor(p => p.Phone).NotEmpty().WithMessage("Phone number is required.").Matches("^[0-9]{10}$").WithMessage("Phone number must be 10 digits with no special characters.");

            RuleFor(p => p.DateOfJoining).Must(p => p >= new DateOnly(1980, 5, 15)).WithMessage("Joiing date must be greater then or equal to 5 May 1980");
        }
    }
}
