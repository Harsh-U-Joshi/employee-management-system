using Employee.Management.Application.DTOs.Auth;
using MediatR;

namespace Employee.Management.Application.Features.Auth.Requests.Commands
{
    public class AuthenticateUserCommand : IRequest<BaseCommandResponse>
    {
        public AuthRequestDto authRequestDto { get; set; }
    }
}
