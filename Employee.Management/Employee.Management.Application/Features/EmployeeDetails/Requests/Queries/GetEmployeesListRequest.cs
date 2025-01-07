using Employee.Management.Application.Models;
using Employee.Management.Domain.Enums;
using MediatR;

namespace Employee.Management.Application.Features.EmployeeDetails.Requests.Queries
{
    public class GetEmployeesListRequest : IRequest<BaseCommandResponse>
    {
        public QueryParamModel queryParamModel { get; set; }
    }

    public class EmployeeBasicDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string Status { get; set; }
    }
}
