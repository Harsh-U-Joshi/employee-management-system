using Employee.Management.Application.Features.EmployeeDetails.Requests.Queries;
using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities;

namespace Employee.Management.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IGenericRespositorary<EmployeeDetail>
    {
        Task<GridDataResponse> GetAllEmployeeDetails(GetEmployeesListRequest request);
        Task<bool> CheckEmployeeEmailExists(string email);
    }
}
