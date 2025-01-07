using Employee.Management.Domain.Enums;

namespace Employee.Management.Application.DTOs.Employee
{
    public class UpdateEmployeeDto : IEmployeeDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string DepartmentId { get; set; }
        public string PositionId { get; set; }
        public EmployeeStatus Status { get; set; }
        public string EmployeeId { get; set; }
    }
}
