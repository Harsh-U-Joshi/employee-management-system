using MediatR;

namespace Employee.Management.Application.Features.EmployeeDetails.Requests.Queries
{
    public class GetEmployeeDetailsRequest : IRequest<BaseCommandResponse>
    {
        public string EmployeeId { get; set; }
    }
}
