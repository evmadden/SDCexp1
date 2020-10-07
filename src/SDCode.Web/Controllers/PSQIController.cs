using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class PSQIController : Controller
    {
        private readonly ILogger<PSQIController> _logger;

        public PSQIController(ILogger<PSQIController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(string participantID)
        {
            return View(new PsqiIndexViewModel(participantID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
