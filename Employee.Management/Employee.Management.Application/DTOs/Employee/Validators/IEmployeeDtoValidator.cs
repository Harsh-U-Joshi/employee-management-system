using Employee.Management.Application.Contracts.Persistence;
using FluentValidation;

namespace Employee.Management.Application.DTOs.Employee.Validators
{
    public class IEmployeeDtoValidator : AbstractValidator<IEmployeeDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IPositionRepository _positionRepository;

        public IEmployeeDtoValidator(IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
        {
            CascadeMode = CascadeMode.Stop;

            _departmentRepository = departmentRepository;

            _positionRepository = positionRepository;

            RuleFor(p => p.FirstName).NotNull().NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.MiddleName).NotNull().NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.LastName).NotNull().NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.DateOfBirth).Must(p => p.Year >= 1980).WithMessage("The year must be 1980 or later");

            RuleFor(p => p.Status).IsInEnum().WithMessage("Employee status is not valid");

            RuleFor(p => p.DepartmentId).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) =>
            {
                   return await _departmentRepository.Exists(id);
               })
               .WithMessage("Department does not exists");

            RuleFor(p => p.PositionId)
               .NotNull().NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) =>
            {
                   return await _positionRepository.Exists(id);
               })
               .WithMessage("Position does not exists");
        }
    }
}
