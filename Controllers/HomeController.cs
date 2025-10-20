using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContractMonthlyClaimSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Index()
    {
        //create an instance for the all_queries with object name create
        Register create = new Register();

        //call the create_table method
        create.Create_Lecturer_table();
        return View();
    }
    [HttpGet]
    public IActionResult Index(Register user)
    {
        if (ModelState.IsValid)
        {
            Register register = new Register();
            register.Store_Lecturer(user.name, user.surname, user.email, user.password, user.role);
        }
        return View(user);
    }


    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Privacy(Login user)
    {
        //making sure whatever you submit all rules are met, eg. name and age are rquired if user submits for with  only one of these it wont submit the form
        if (ModelState.IsValid)
        {
            Login getLecturer = new Login();

            bool found = getLecturer.searchLecturer(user.name, user.surname, user.email, user.password, user.role);
            if (found)
            {
                return RedirectToAction("HomePg", "Home");
            }
            else
            {
                Console.WriteLine("Lecturer Not Found");
            }
                
        }
        return View(user);
    }
    public IActionResult HomePg()
    {
        // Show the claim submission form
        return View("~/Views/Home/HomePg.cshtml");
    }

    public IActionResult Approve()
    {
        // Show the final approve page for Academic Manager
        return View("~/Views/Home/Approve.cshtml");
    }

    public IActionResult Claim()
    {
        // Show the claim submission form
        return View("~/Views/Home/Claim.cshtml");
    }

    public IActionResult TrackClaim()
    {
        // Show the claim tracking table (hard-coded data)
        return View("~/Views/Home/TrackClaim.cshtml");
    }

    public IActionResult Review()
    {
        // Show the pre-approve page for Programme Coordinator
        return View("~/Views/Home/Review.cshtml");
    }   


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
