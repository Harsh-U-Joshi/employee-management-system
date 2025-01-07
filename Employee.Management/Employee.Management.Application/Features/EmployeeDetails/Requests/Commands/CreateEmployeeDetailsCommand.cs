using Employee.Management.Application.DTOs.Employee;
using MediatR;

namespace Employee.Management.Application.Features.EmployeeDetails.Requests.Commands
{
    public class CreateEmployeeDetailsCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmployeeDto createEmployeeDto { get; set; }

        public string loggedInUserId { get; set; }
    }
}
