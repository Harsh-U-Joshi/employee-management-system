using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Persistence.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public PositionRepository(EmployeeManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CommonDropDownResponse>> GetPositionDropDown()
        {
            return await _dbContext.Position.AsNoTracking().Select(x => new CommonDropDownResponse() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
    }
}
