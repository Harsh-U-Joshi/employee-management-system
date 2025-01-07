using Employee.Management.Application.DTOs.Employee;
using MediatR;

namespace Employee.Management.Application.Features.EmployeeDetails.Requests.Commands
{
    public class DeleteEmployeeDetailsCommand : IRequest<BaseCommandResponse>
    {
        public DeleteEmployeeDto deleteEmployeeDto { get; set; }
        public string loggedInUserId { get; set; }
    }
}
