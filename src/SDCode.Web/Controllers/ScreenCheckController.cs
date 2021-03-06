using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Classes;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class ScreenCheckController : Controller
    {
        private readonly ILogger<ScreenCheckController> _logger;
        private ITestNameGetter _testNameGetter;

        public ScreenCheckController(ILogger<ScreenCheckController> logger, ITestNameGetter testNameGetter)
        {
            _logger = logger;
            _testNameGetter = testNameGetter;
        }

        [HttpPost]
        public IActionResult Index(string participantID, string nextActionAfterScreenCheck)
        {
            var viewModel = new ScreenCheckIndexViewModel(participantID, nextActionAfterScreenCheck);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
