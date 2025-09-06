using Microsoft.AspNetCore.Mvc;

namespace CMCS.Prototype.Controllers
{
    // Handles login and register pages
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            // Show the Login.cshtml view
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Prototype only: ignore input, skip validation
            // Redirect user to Home/Dashboard after pressing "Login"
            return RedirectToAction("Dashboard", "Home");
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            // Show the Register.cshtml view
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string fullName, string email, string password)
        {
            // Prototype only: no saving of data
            // After pressing "Register", redirect back to Login page
            return RedirectToAction("Login");
        }
    }
}