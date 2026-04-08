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
            /*
                REPOSITORY LOGIC + SESSION HANDLING LOGIC GOES HERE
            */

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            /*
                REPOSITORY LOGIC + SESSION HANDLING LOGIC GOES HERE
            */

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
