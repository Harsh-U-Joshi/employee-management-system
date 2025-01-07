using AutoMapper;
using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.DTOs.Auth.Validators;
using Employee.Management.Application.Exceptions;
using Employee.Management.Application.Features.Auth.Requests.Commands;
using Employee.Management.Domain.Entities.ApplicationUser;
using MediatR;
using System.Net;

namespace Employee.Management.Application.Features.Auth.Handlers.Commands
{
    public class CreateUserDtoCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public CreateUserDtoCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;

            _userRepository = userRepository;

        }

        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserRequestDtoValidator();

            var validationResult = await validator.ValidateAsync(request.createUserRequestDto);

            if (!validationResult.IsValid)
                throw new GenericException(HttpStatusCode.BadRequest.GetHashCode(), validationResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault());

            var response = new BaseCommandResponse();

            var existingUser = await _userRepository.GetUserByEmail(request.createUserRequestDto.Email);

            if (existingUser is not null)
                throw new GenericException(HttpStatusCode.Conflict.GetHashCode(), "A user with this email already exists.");

            ApplicationUser dbEntity = _mapper.Map<ApplicationUser>(request.createUserRequestDto);

            await _userRepository.RegisterAsync(dbEntity);

            response.Message = "User created successfully";

            return response;
        }
    }
}
