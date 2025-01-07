using Employee.Management.API.Common;
using Employee.Management.Application.Contracts.Identity;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.DTOs.Auth;
using Employee.Management.Domain.Entities.ApplicationUser;
using Employee.Management.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Identity.Services;

public class UserService : GenericRepository<ApplicationUser>, IUserService
{
    public EmployeeManagementIdentityDbContext _dbContext { get; set; }

    public UserService(EmployeeManagementIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {
        return await _dbContext.ApplicationUser.FirstOrDefaultAsync(x => !x.IsDeleted && x.Email == email);
    }

    public async Task<RegistrationResponse> Register(CreateUserRequestDto request)
    {
        var existingUser = await FindByEmailAsync(request.UserName);

        if (existingUser is not null)
            throw new GenericResponse()

            throw new Exception($"Username '{request.UserName}' already exists.");

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail is not null)
            throw new Exception($"Email {request.Email} already exists.");

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");

            return new RegistrationResponse() { UserId = user.Id };
        }
        else
            throw new Exception($"{result.Errors}");

    }
}
