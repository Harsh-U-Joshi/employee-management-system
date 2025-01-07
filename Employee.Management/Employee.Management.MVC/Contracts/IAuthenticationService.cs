using Employee.Management.MVC.Models.Auth;

namespace Employee.Management.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<(bool isSuccess, string Message)> Register(RegisterUserRequest request, CancellationToken cancellationToken);
        Task<(bool isSuccess, string Message)> Authenticate(string email, string password, CancellationToken cancellationToken);
        Task Logout();
    }
}
