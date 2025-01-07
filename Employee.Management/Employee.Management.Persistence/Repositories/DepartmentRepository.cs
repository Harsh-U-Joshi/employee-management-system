using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Persistence.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public DepartmentRepository(EmployeeManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CommonDropDownResponse>> GetDepartmentDropDown()
        {
            return await _dbContext.Department.AsNoTracking().Select(x => new CommonDropDownResponse() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
