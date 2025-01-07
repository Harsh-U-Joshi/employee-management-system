using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Queries;
using MediatR;

namespace Employee.Management.Application.Features.EmployeeDetails.Handlers.Queries
{
    public class GetEmployeesListRequestHandler : IRequestHandler<GetEmployeesListRequest, BaseCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesListRequestHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<BaseCommandResponse> Handle(GetEmployeesListRequest request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            response.Data = await _employeeRepository.GetAllEmployeeDetails(request);

            return response;
        }
    }
}
