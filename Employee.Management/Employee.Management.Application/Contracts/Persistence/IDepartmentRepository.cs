using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities;

namespace Employee.Management.Application.Contracts.Persistence
{
    public interface IDepartmentRepository : IGenericRespositorary<Department>
    {
        Task<List<CommonDropDownResponse>> GetDepartmentDropDown();
    }
}
