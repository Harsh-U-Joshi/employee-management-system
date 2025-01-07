using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Domain.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public UserRepository(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser?> GetUserByEmail(string email)
        {
            return await _dbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> RegisterAsync(ApplicationUser applicationUser)
        {
            await _dbContext.ApplicationUser.AddAsync(applicationUser);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
