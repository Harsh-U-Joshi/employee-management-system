using System.ComponentModel.DataAnnotations;

namespace Employee.Management.MVC.Models.Auth
{
    public class AuthRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
