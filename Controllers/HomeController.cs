using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
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
