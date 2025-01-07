using Employee.Management.Application.DTOs.Auth;
using MediatR;

namespace Employee.Management.Application.Features.Auth.Requests.Commands
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserRequestDto createUserRequestDto { get; set; }
    }
}
