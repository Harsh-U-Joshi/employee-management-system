using Employee.Management.Domain.Entities.Common;
using Employee.Management.Domain.Enums;

namespace Employee.Management.Domain.Entities
{
    public class EmployeeDetail : BaseDomainEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfJoining { get; set; }
        public Gender Gender { get; set; }
        public string DepartmentId { get; set; }
        public string PositionId { get; set; }
        public EmployeeStatus Status { get; set; }
    }

}