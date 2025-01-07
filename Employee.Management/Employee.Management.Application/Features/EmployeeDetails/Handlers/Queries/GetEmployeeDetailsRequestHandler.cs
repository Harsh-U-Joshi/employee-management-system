using AutoMapper;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.DTOs.Employee;
using Employee.Management.Application.Exceptions;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Queries;
using Employee.Management.Domain.Entities;
using MediatR;
using System.Net;

namespace Employee.Management.Application.Features.EmployeeDetails.Handlers.Queries
{
    public class GetEmployeeDetailsRequestHandler : IRequestHandler<GetEmployeeDetailsRequest, BaseCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        public GetEmployeeDetailsRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;

            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(GetEmployeeDetailsRequest request, CancellationToken cancellationToken)
        {
            EmployeeDetail? dbRecord = await _employeeRepository.GetAsync(request.EmployeeId);

            BaseCommandResponse response = new BaseCommandResponse();

            if (dbRecord is null)
                throw new GenericException(HttpStatusCode.NotFound.GetHashCode(), "Employee Details Not Found");

            response.Data = _mapper.Map<UpdateEmployeeDto>(dbRecord);

            return response;
        }
    }
}
