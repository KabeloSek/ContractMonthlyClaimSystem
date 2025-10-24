using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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

    [HttpGet]
    public IActionResult Index()
    {
        //create an instance for the Register with object name create
        Register create = new Register();

        //call the create_table method
        create.Create_Lecturer_table();
        return View();
    }
    [HttpPost]
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
            Login login = new Login();

            bool found = login.searchLogin(user.email, user.password, user.role);
            if (found)
            {
                //redirect based on role
                switch (user.role.ToLower())
                {
                    case "lecturer":
                        return RedirectToAction("HomePg", "Home");
                    case "program_coordinator":
                        return RedirectToAction("Review", "Home");
                    case "program_manager":
                        return RedirectToAction("Approve", "Home");
                    default:
                        Console.WriteLine("Role not recognized");
                        return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                Console.WriteLine("User Not Found");

            }
        }
        return View(user);
    }
    public IActionResult HomePg()
    {
        // Show the claim submission form
        return View("~/Views/Home/HomePg.cshtml");
    }
    [HttpGet]
    public IActionResult Claim()
    {
        //create an instance for the Claim with object name claim
        All_Queries claim = new All_Queries();

        //call the create_table method
        claim.createClaimsTable();

        // Show the claim submission form
        return View("~/Views/Home/Claim.cshtml", claim);
    }
    [HttpPost]
    public IActionResult Claim(All_Queries claim)
    {
        if (ModelState.IsValid)
        {
            claim.Store_claims(claim.name, claim.sessions, claim.hoursWorked, claim.hourlyRate,claim.document);
            Console.WriteLine("Claim submitted successfully.");
        }
        return View("~/Views/Home/Claim.cshtml", claim);
    }

    [HttpGet]
    public IActionResult Review()
    {
        //create an instance for claim
        All_Queries claim = new All_Queries();

        //call the get all claims method
        List<All_Queries> claims = claim.GetAllClaims();

        // Show the pre-approve page for Programme Coordinator
        return View("~/Views/Home/Review.cshtml", claims);
    }
   
    [HttpGet]
    public IActionResult Approve()
    {
        All_Queries claim = new All_Queries();

        List<All_Queries> claims = claim.GetAllClaims();
        // Show the final approve page for Academic Manager
        return View("~/Views/Home/Approve.cshtml", claims);
    }
    
   
    public IActionResult TrackClaim()
    {
        All_Queries claim = new All_Queries();

        List<All_Queries> claims = claim.GetAllClaims();
        // Show the claim tracking table (hard-coded data)
        return View("~/Views/Home/TrackClaim.cshtml", claims);
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}