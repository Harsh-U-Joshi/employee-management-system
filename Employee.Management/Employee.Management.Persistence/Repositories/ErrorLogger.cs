using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Domain.Entities.Common;

namespace Employee.Management.Persistence.Repositories
{
    public class ErrorLogger : IErrorLogger
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public ErrorLogger(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveErrorLogs(ErrorLog errorLog)
        {
            // Assuming you have a DbSet<ErrorLog> in your DbContext
            await _dbContext.ErrorLog.AddAsync(errorLog);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
