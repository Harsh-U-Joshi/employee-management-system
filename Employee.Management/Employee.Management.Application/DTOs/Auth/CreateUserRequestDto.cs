namespace Employee.Management.Application.DTOs.Auth;

public class CreateUserRequestDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
}
