using Employee.Management.MVC.Utitlies;
using System.ComponentModel.DataAnnotations;

namespace Employee.Management.MVC.Models.Employee
{
    public class UpdateEmployee
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string DepartmentId { get; set; }
        public string PositionId { get; set; }
        public Gender Gender { get; set; }
        public EmployeeStatus Status { get; set; }
        public string EmployeeId { get; set; }
    }


    public class UpdateEmployeeViewModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle Name is required.")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "DOB is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [Display(Name = "Position")]
        public string PositionId { get; set; }

        public Gender Gender { get; set; }
        public EmployeeStatus Status { get; set; }
        public string EmployeeId { get; set; }
    }
}
