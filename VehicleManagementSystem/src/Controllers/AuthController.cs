using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.src.Repositories;

namespace VehicleManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly STAFFREPO _authRepository;

        public AuthController(STAFFREPO authRepository)
        {
            _authRepository = authRepository;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Invalid input";
                return View();
            }

            var user = await _authRepository.CheckUserCredentials(username, password);

            if (user == null)
            {
                ViewBag.Error = "Username or Password incorrect";
                return View();
            }

            HttpContext.Session.SetInt32("UserID", user.ID);
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            User user = new User { Username = username, Password = password};

            await _userRepository.AddUser(user);

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
