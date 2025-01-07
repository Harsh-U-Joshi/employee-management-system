using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Features.Departments.Requests.Queries;
using MediatR;

namespace Employee.Management.Application.Features.Departments.Handlers.Queries
{
    public class GetAllDepartmentRequestHandler : IRequestHandler<GetAllDepartmentRequest, BaseCommandResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetAllDepartmentRequestHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<BaseCommandResponse> Handle(GetAllDepartmentRequest request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            response.Data = await _departmentRepository.GetDepartmentDropDown();

            return response;
        }

    }
}
