using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Classes;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class SleepQuestionsController : Controller
    {
        private readonly ILogger<SleepQuestionsController> _logger;
        private readonly ISleepQuestionsRepository _sleepQuestionsRepository;
        private readonly ITestNameGetter _testNameGetter;

        public SleepQuestionsController(ILogger<SleepQuestionsController> logger, ISleepQuestionsRepository sleepQuestionsRepository, ITestNameGetter testNameGetter)
        {
            _logger = logger;
            _sleepQuestionsRepository = sleepQuestionsRepository;
            _testNameGetter = testNameGetter;
        }

        [HttpPost]
        public IActionResult Index(string participantID)
        {
            return View(new SleepQuestionsIndexViewModel(participantID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
