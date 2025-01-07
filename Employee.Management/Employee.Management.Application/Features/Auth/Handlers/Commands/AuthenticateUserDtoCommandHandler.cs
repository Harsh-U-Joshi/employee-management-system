using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Application.Exceptions;
using Employee.Management.Application.Features.Auth.Requests.Commands;
using Employee.Management.Application.Models;
using Employee.Management.Domain.Entities.ApplicationUser;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Employee.Management.Application.Utilities;
using Employee.Management.Application.DTOs.Auth.Validators;

namespace Employee.Management.Application.Features.Auth.Handlers.Commands
{
    public class AuthenticateUserDtoCommandHandler : IRequestHandler<AuthenticateUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        private readonly JwtSettings _jwtSettings;

        private string _privateKey;

        public AuthenticateUserDtoCommandHandler(IConfiguration configuration, IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _privateKey = configuration["PasswordProtection:PrivateKey"];

            _userRepository = userRepository;

            _jwtSettings = jwtSettings.Value;
        }

        public async Task<BaseCommandResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new AuthRequestDtoValidator();

            var validationResult = await validator.ValidateAsync(request.authRequestDto);

            if (!validationResult.IsValid)
                throw new GenericException(HttpStatusCode.BadRequest.GetHashCode(), validationResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault());

            var response = new BaseCommandResponse();

            string decryptedPassword = SymmetricEncryptionHelper.Decrypt(request.authRequestDto.Password, _privateKey);

            var user = await _userRepository.GetUserByEmail(request.authRequestDto.Email);

            if (user is null)
                throw new GenericException(HttpStatusCode.NotFound.GetHashCode(), "User not found");

            string userPassword = SymmetricEncryptionHelper.Decrypt(user.PasswordHash, _privateKey);

            bool isPasswordValid = string.Equals(decryptedPassword, userPassword);

            if (!isPasswordValid)
                throw new GenericException(HttpStatusCode.Unauthorized.GetHashCode(), "Invalid email or password");

            JwtSecurityToken jwtSecurityToken = GenerateToken(user);

            AuthResponse authResponse = new()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ExpiresIn = _jwtSettings.DurationInSeconds,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            response.Data = authResponse;

            return response;
        }

        private JwtSecurityToken GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_jwtSettings.DurationInSeconds),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }


}
