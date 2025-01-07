using Employee.Management.Application.Features.EmployeeDetails.Requests.Commands;
using Employee.Management.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Employee.Management.API.Common;
using Employee.Management.Application.Features.Departments.Requests.Queries;

namespace Employee.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        private GenericResponse response;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;

            response = new GenericResponse();
        }

        [HttpGet("all")]
        public async Task<ActionResult<BaseCommandResponse>> Get()
        {
            var command = new GetAllDepartmentRequest();

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Data = baseCommandResponse.Data;

            return Ok(response);
        }
    }
}
