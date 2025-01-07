using Employee.Management.API.Common;
using Employee.Management.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Employee.Management.Application.DTOs.Auth;
using Employee.Management.Application.Features.Auth.Requests.Commands;

namespace Employee.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        private GenericResponse response;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;

            response = new GenericResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseCommandResponse>> Login([FromBody] AuthRequestDto authRequestDto)
        {
            var command = new AuthenticateUserCommand { authRequestDto = authRequestDto };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Message = baseCommandResponse.Message;

            response.Data = baseCommandResponse.Data;

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseCommandResponse>> Register([FromBody] CreateUserRequestDto createUserRequestDto)
        {
            var command = new CreateUserCommand { createUserRequestDto = createUserRequestDto };

            BaseCommandResponse baseCommandResponse = await _mediator.Send(command);

            response.Message = baseCommandResponse.Message;

            return Ok(response);
        }
    }
}
