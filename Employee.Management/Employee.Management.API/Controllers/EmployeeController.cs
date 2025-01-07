using Employee.Management.API.Common;
using Employee.Management.Application;
using Employee.Management.Application.DTOs.Employee;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Commands;
using Employee.Management.Application.Features.EmployeeDetails.Requests.Queries;
using Employee.Management.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        private GenericResponse response;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;

            response = new GenericResponse();
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var command = new CreateEmployeeDetailsCommand { createEmployeeDto = createEmployeeDto, loggedInUserId = UserId() };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Message = baseCommandResponse.Message;

            response.Data = baseCommandResponse.Data;

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var command = new UpdateEmployeeDetailsCommand { updateEmployeeDto = updateEmployeeDto, loggedInUserId = UserId() };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Message = baseCommandResponse.Message;

            return Ok(response);
        }

        [HttpDelete("{employee_id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(string employee_id)
        {
            var command = new DeleteEmployeeDetailsCommand { deleteEmployeeDto = new() { EmployeeId = employee_id }, loggedInUserId = UserId() };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Message = baseCommandResponse.Message;

            return Ok(response);
        }

        [HttpGet("{employee_id}")]
        public async Task<ActionResult<BaseCommandResponse>> GetEmployeeById(string employee_id)
        {
            var command = new GetEmployeeDetailsRequest { EmployeeId = employee_id };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Data = baseCommandResponse.Data;

            return Ok(response);
        }

        [HttpPost("grid")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllEmployee(QueryParamModel queryParamModel)
        {
            var command = new GetEmployeesListRequest() { queryParamModel = queryParamModel };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Data = baseCommandResponse.Data;

            return Ok(response);
        }

        protected string UserId()
        {
            string userId;
            userId = User == null ? "" : User.Identities.First().Claims.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
            return userId;
        }
    }
}
