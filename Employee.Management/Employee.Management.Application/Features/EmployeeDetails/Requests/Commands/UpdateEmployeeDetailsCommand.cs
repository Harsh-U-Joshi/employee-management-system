using Employee.Management.Application.DTOs.Employee;
using MediatR;

namespace Employee.Management.Application.Features.EmployeeDetails.Requests.Commands
{
    public class UpdateEmployeeDetailsCommand : IRequest<BaseCommandResponse>
    {
        public UpdateEmployeeDto updateEmployeeDto { get; set; }

        public string loggedInUserId { get; set; }
    }
}
