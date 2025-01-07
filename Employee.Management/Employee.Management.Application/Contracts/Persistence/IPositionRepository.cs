using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities;

namespace Employee.Management.Application.Contracts.Persistence
{
    public interface IPositionRepository : IGenericRespositorary<Position>
    {
        Task<List<CommonDropDownResponse>> GetPositionDropDown();
    }
}
