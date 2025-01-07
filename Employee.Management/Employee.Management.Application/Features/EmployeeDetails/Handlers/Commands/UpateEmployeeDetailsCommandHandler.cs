using AutoMapper;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.DTOs.Employee.Validators;
using Employee.Management.Application.Exceptions;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Commands;
using MediatR;
using System.Net;

namespace Employee.Management.Application.Features.EmployeeDetails.Handlers.Commands
{
    public class UpateEmployeeDetailsCommandHandler : IRequestHandler<UpdateEmployeeDetailsCommand, BaseCommandResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IPositionRepository _positionRepository;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        public UpateEmployeeDetailsCommandHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmployeeDetailsCommand request, CancellationToken cancellationToken)
        {
            string userId = request.loggedInUserId;

            var response = new BaseCommandResponse();

            var validator = new UpdateEmployeeDtoValidator(_departmentRepository, _positionRepository);

            var validationResult = await validator.ValidateAsync(request.updateEmployeeDto);

            if (!validationResult.IsValid)
                throw new GenericException(HttpStatusCode.BadRequest.GetHashCode(), validationResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault());

            var dbRecord = await _employeeRepository.GetAsync(request.updateEmployeeDto.EmployeeId);

            if (dbRecord is null)
                throw new GenericException(HttpStatusCode.NotFound.GetHashCode(), "Employee Not Found");

            _mapper.Map(request.updateEmployeeDto, dbRecord);

            await _employeeRepository.UpdateAsync(userId, dbRecord);

            response.Message = "Employee updated successfully";

            return response;
        }
    }
}
