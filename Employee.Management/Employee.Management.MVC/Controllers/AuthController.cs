using Employee.Management.MVC.Contracts;
using Employee.Management.MVC.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Management.MVC.Controllers
{
    public class AuthController : Controller
    {
        public IAuthenticationService _authenticationService { get; }
        public ILocalStorageService _localStorageService { get; set; }

        public AuthController(IAuthenticationService authenticationService, ILocalStorageService localStorageService)
        {
            _authenticationService = authenticationService;

            _localStorageService = localStorageService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await _authenticationService.Authenticate(request.Email, request.Password, cancellationToken);

            if (!httpResponse.isSuccess)
                ViewBag.ErrorMessage = httpResponse.Message;
            else
            {
                return RedirectToAction("Index", "Employee");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await _authenticationService.Register(request, cancellationToken);

            if (!httpResponse.isSuccess)
                ViewBag.ErrorMessage = httpResponse.Message;
            else
                return RedirectToAction("Login", "Auth");

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();

            return RedirectToAction("Index", "Home");

        }
    }
}
