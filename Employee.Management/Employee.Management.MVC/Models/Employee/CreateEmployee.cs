using Employee.Management.MVC.Utitlies;
using System.ComponentModel.DataAnnotations;

namespace Employee.Management.MVC.Models.Employee
{
    public class CreateEmployee
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
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [Display(Name = "Position")]
        public string PositionId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Employee Status")]
        public EmployeeStatus Status { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "DOJ is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Joining")]
        public DateOnly DateOfJoining { get; set; }
    }
}
