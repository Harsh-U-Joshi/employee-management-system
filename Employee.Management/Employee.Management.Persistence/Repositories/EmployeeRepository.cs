using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Queries;
using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities;
using Employee.Management.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<EmployeeDetail>, IEmployeeRepository
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public EmployeeRepository(EmployeeManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GridDataResponse> GetAllEmployeeDetails(GetEmployeesListRequest request)
        {
            GridDataResponse response = new() { PageIndex = request.queryParamModel.PageIndex };

            var sqlQuery = (from ed in _dbContext.EmployeeDetail
                            join dp in _dbContext.Department on ed.DepartmentId equals dp.Id
                            join pt in _dbContext.Position on ed.PositionId equals pt.Id
                            where !ed.IsDeleted && !dp.IsDeleted && !pt.IsDeleted
                            select new EmployeeBasicDetails()
                            {
                                Id = ed.Id,
                                Name = string.Concat(ed.FirstName, " ", ed.LastName),
                                Email = ed.Email,
                                Phone = ed.Phone,
                                DepartmentName = dp.Name,
                                PositionName = pt.Name,
                                Status = ed.Status.ToString(),
                            }).AsNoTracking();

            response.RecordsTotalCount = await sqlQuery.CountAsync();

            if (!string.IsNullOrWhiteSpace(request.queryParamModel.Search))
                sqlQuery = sqlQuery.Where(w => w.Name.ToLower().Contains(request.queryParamModel.Search.ToLower()));

            response.RecordsFilteredCount = await sqlQuery.CountAsync();

            if (!string.IsNullOrEmpty(request.queryParamModel.SortColumn))
            {
                OrderDir sortOrder = (OrderDir)Enum.Parse(typeof(OrderDir), request.queryParamModel.OrderBy.ToUpper());
                if (request.queryParamModel.SortColumn == "name")
                    sqlQuery = sortOrder == OrderDir.ASC ? sqlQuery.OrderBy(x => x.Name) : sqlQuery.OrderByDescending(x => x.Name);
                else if (request.queryParamModel.SortColumn == "email".ToLower())
                    sqlQuery = sortOrder == OrderDir.ASC ? sqlQuery.OrderBy(x => x.Email) : sqlQuery.OrderByDescending(x => x.Email);
                else if (request.queryParamModel.SortColumn == "phone".ToLower())
                    sqlQuery = sortOrder == OrderDir.ASC ? sqlQuery.OrderBy(x => x.Phone) : sqlQuery.OrderByDescending(x => x.Phone);
                else if (request.queryParamModel.SortColumn == "departmentName")
                    sqlQuery = sortOrder == OrderDir.ASC ? sqlQuery.OrderBy(x => x.DepartmentName) : sqlQuery.OrderByDescending(x => x.DepartmentName);
                else if (request.queryParamModel.SortColumn == "positionName")
                    sqlQuery = sortOrder == OrderDir.ASC ? sqlQuery.OrderBy(x => x.PositionName) : sqlQuery.OrderByDescending(x => x.PositionName);
                else if (request.queryParamModel.SortColumn == "status")
                    sqlQuery = sortOrder == OrderDir.ASC ? sqlQuery.OrderBy(x => x.Status) : sqlQuery.OrderByDescending(x => x.Status);
            }
            else
                sqlQuery = sqlQuery.OrderBy(x => x.Name);

            response.Data = await sqlQuery.Skip(request.queryParamModel.Skip).Take(request.queryParamModel.PageSize).ToListAsync();

            return response;
        }

        public async Task<bool> CheckEmployeeEmailExists(string email)
        {
            return await _dbContext.EmployeeDetail.AsNoTracking().AnyAsync(x => x.Email == email && !x.IsDeleted);
        }
    }
}
