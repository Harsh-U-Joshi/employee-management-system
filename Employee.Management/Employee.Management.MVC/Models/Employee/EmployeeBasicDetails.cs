using Employee.Management.MVC.Utitlies;

namespace Employee.Management.MVC.Models.Employee
{
    public class EmployeeBasicDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string Status { get; set; }
    }

    public class EmployeeDetails
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
