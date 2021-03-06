using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Classes;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class ImageCheckController : Controller
    {
        private readonly ILogger<ImageCheckController> _logger;

        public ImageCheckController(ILogger<ImageCheckController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(string participantID, Sleepinesses? stanford, string nextActionAfterImageCheck, bool showSpacebarOrientation)
        {
            var viewModel = new ImageCheckIndexViewModel(participantID, stanford, nextActionAfterImageCheck, showSpacebarOrientation);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
