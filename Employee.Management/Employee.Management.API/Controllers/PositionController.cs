using Employee.Management.Application.Features.Departments.Requests.Queries;
using Employee.Management.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Employee.Management.API.Common;
using Employee.Management.Application.Features.Positions.Requests.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Employee.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IMediator _mediator;

        private GenericResponse response;

        public PositionController(IMediator mediator)
        {
            _mediator = mediator;

            response = new GenericResponse();
        }

        [HttpGet("all")]
        public async Task<ActionResult<BaseCommandResponse>> Get()
        {
            var command = new GetAllPositionRequest();

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Data = baseCommandResponse.Data;

            return Ok(response);
        }
    }
}
