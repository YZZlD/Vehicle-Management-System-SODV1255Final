using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        //private readonly AuthRepository _authRepository;

        // public AuthController(AuthRepository authRepository)
        // {
        //     _authRepository = authRepository;
        // }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Invalid input";
                return View();
            }

            User user = await _userRepository.CheckUserCredentials(username, password);

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
