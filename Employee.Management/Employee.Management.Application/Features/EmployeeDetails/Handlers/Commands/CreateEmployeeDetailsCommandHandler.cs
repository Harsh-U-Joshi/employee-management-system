using AutoMapper;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.DTOs.Employee.Validators;
using Employee.Management.Application.Exceptions;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Commands;
using Employee.Management.Domain.Entities;
using MediatR;
using System.Net;

namespace Employee.Management.Application.Features.EmployeeDetails.Handlers.Commands
{
    public class CreateEmployeeDetailsCommandHandler : IRequestHandler<CreateEmployeeDetailsCommand, BaseCommandResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IPositionRepository _positionRepository;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        public CreateEmployeeDetailsCommandHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEmployeeDetailsCommand request, CancellationToken cancellationToken)
        {
            string userId = request.loggedInUserId;

            var response = new BaseCommandResponse();

            var validator = new CreateEmployeeDtoValidator(_departmentRepository, _positionRepository);

            var validationResult = await validator.ValidateAsync(request.createEmployeeDto);

            if (!validationResult.IsValid)
                throw new GenericException(HttpStatusCode.BadRequest.GetHashCode(), validationResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault());

            if (await _employeeRepository.CheckEmployeeEmailExists(request.createEmployeeDto.Email))
                throw new GenericException(HttpStatusCode.BadRequest.GetHashCode(), "Please use different email");

            var employeeDetails = _mapper.Map<EmployeeDetail>(request.createEmployeeDto);

            var dbResult = await _employeeRepository.AddAsync(userId, employeeDetails);

            response.Message = "Employee created successfully";

            response.Data = new { employeeId = employeeDetails.Id };

            return response;
        }
    }
}
