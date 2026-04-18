using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.src.Models;
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
            //Basic validation for username and password form fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Invalid input";
                return View();
            }

            var user = await _authRepository.checkcreds(username, password);

            //Rereturn the view if the credentials don't register in the repository with error information
            if (user == null)
            {
                ViewBag.Error = "Username or Password Incorrect";
                return View();
            }

            //Set values in session context for user validation
            HttpContext.Session.SetInt32("UserID", user.staffid);
            HttpContext.Session.SetString("Username", user.username);
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            STAFFMODEL user = new STAFFMODEL { username = username, password = password};

            _authRepository.AddUser(user);

            return RedirectToAction("Login");
        }

        //We simply clear the session to logout a user
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
