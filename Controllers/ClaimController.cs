using Microsoft.AspNetCore.Mvc;

namespace CMCS.Prototype.Controllers
{
    // Handles all claim-related pages
    public class ClaimController : Controller
    {
        // GET: /Claim/ClaimForm
        public IActionResult ClaimForm()
        {
            // Show the claim submission form
            return View();
        }

        // GET: /Claim/Track
        public IActionResult Track()
        {
            // Show the claim tracking table (hard-coded data)
            return View();
        }

        // GET: /Claim/PreApprove
        public IActionResult PreApprove()
        {
            // Show the pre-approve page for Programme Coordinator
            return View();
        }

        // GET: /Claim/Approve
        public IActionResult Approve()
        {
            // Show the final approve page for Academic Manager
            return View();
        }
    }
}