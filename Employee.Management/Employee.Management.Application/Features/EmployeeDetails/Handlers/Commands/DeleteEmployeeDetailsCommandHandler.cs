using AutoMapper;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.DTOs.Employee.Validators;
using Employee.Management.Application.Exceptions;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Commands;
using MediatR;
using System.Net;

namespace Employee.Management.Application.Features.EmployeeDetails.Handlers.Commands
{
    public class DeleteEmployeeDetailsCommandHandler : IRequestHandler<DeleteEmployeeDetailsCommand, BaseCommandResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IPositionRepository _positionRepository;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;

        public DeleteEmployeeDetailsCommandHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmployeeDetailsCommand request, CancellationToken cancellationToken)
        {
            string userId = request.loggedInUserId;

            var response = new BaseCommandResponse();

            var validator = new DeleteEmployeeDtoValidator();

            var validationResult = await validator.ValidateAsync(request.deleteEmployeeDto);

            if (!validationResult.IsValid)
                throw new GenericException(HttpStatusCode.BadRequest.GetHashCode(), validationResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault());

            var employeeDetails = await _employeeRepository.GetAsync(request.deleteEmployeeDto.EmployeeId);

            if (employeeDetails is null)
                throw new GenericException(HttpStatusCode.NotFound.GetHashCode(), "Employee Not Found");

            await _employeeRepository.DeleteAsync(userId,request.deleteEmployeeDto.EmployeeId);

            response.Message = "Employee deleted successfully";

            return response;
        }
    }
}
