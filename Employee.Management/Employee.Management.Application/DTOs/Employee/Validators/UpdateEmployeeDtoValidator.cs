using Employee.Management.Application.Contracts.Persistence;
using FluentValidation;

namespace Employee.Management.Application.DTOs.Employee.Validators
{
    public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator(IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
        {
            Include(new IEmployeeDtoValidator(departmentRepository, positionRepository));
        }
    }
}
