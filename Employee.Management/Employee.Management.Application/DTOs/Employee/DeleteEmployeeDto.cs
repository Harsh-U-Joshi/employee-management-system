using FluentValidation;

namespace Employee.Management.Application.DTOs.Employee
{
    public class DeleteEmployeeDto : AbstractValidator<DeleteEmployeeDto>
    {
        public string EmployeeId { get; set; }
    }
}
