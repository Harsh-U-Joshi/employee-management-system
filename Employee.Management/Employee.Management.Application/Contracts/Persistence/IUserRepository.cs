using Employee.Management.Domain.Entities.ApplicationUser;

namespace Employee.Management.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<bool> RegisterAsync(ApplicationUser applicationUser);

        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
