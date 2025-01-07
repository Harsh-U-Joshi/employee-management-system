using Employee.Management.MVC.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Employee.Management.MVC.Models.Auth;
using Microsoft.Extensions.Options;
using Employee.Management.MVC.Utitlies;
using Employee.Management.MVC.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Employee.Management.MVC.Services
{
    public class AuthenticationService : Contracts.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILocalStorageService _localStorage;

        private readonly IBaseHttpService _baseHttpService;

        private readonly PasswordProtection _backendConfiguration;

        public AuthenticationService(ILocalStorageService localStorage, IBaseHttpService baseHttpService, IHttpContextAccessor httpContextAccessor, IOptions<PasswordProtection> options)
        {
            _httpContextAccessor = httpContextAccessor;

            _localStorage = localStorage;

            _baseHttpService = baseHttpService;

            _backendConfiguration = options.Value;
        }

        public async Task<(bool isSuccess, string Message)> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Password = SymmetricEncryptionHelper.Encrypt(request.Password, _backendConfiguration.PrivateKey);

                var authenticationResponse = await _baseHttpService.PostAsync<RegisterUserRequest, object>(ApiEndpoints.RegisterUserApi, request, cancellationToken);

                if (!authenticationResponse.Success)
                    return (false, authenticationResponse.Message);
            }
            catch (Exception exception)
            {
                return (false, exception.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool isSuccess, string Message)> Authenticate(string email, string password, CancellationToken cancellationToken)
        {
            try
            {
                string passwordHash = SymmetricEncryptionHelper.Encrypt(password, _backendConfiguration.PrivateKey);

                AuthRequest authenticationRequest = new() { Email = email, Password = passwordHash };

                var authenticationResponse = await _baseHttpService.PostAsync<AuthRequest, AuthResponse>(ApiEndpoints.AuthApi, authenticationRequest, cancellationToken);

                if (authenticationResponse.Success && authenticationResponse.Data is not null)
                {
                    JwtSecurityTokenHandler _tokenHandler = new();

                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Data.Token);

                    var claims = ParseClaims(tokenContent);

                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

                    _localStorage.SetStorageValue("token", authenticationResponse.Data.Token);

                    _localStorage.SetStorageValue("email", authenticationResponse.Data.Email);

                    _httpContextAccessor.HttpContext.Session.SetString("sessionTimeout", DateTime.UtcNow.AddSeconds(authenticationResponse.Data.ExpiresIn - 120).ToString());
                }
                else if (!authenticationResponse.Success)
                    return (false, authenticationResponse.Message);
                else
                    return (false, CommonMessage.ErrorMessage);
            }
            catch (Exception exception)
            {
                return (false, exception.Message);
            }

            return (true, string.Empty);
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token", "email", "sessionTimeout" });

            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
