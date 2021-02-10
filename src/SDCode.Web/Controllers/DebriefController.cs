using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDCode.Web.Classes;
using SDCode.Web.Models;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Controllers
{
    public class DebriefController : Controller
    {
        private readonly ILogger<DebriefController> _logger;
        private readonly IEpworthRepository _epworthRepository;
        private readonly IConfig _config;

        public DebriefController(ILogger<DebriefController> logger, IEpworthRepository epworthRepository, IOptions<Config> config)
        {
            _logger = logger;
            _epworthRepository = epworthRepository;
            _config = config.Value;
        }

        [HttpPost]
        public IActionResult Index(string participantID, ChancesDozing? reading, ChancesDozing? tv, ChancesDozing? publicplace, ChancesDozing? passengercar, ChancesDozing? afternoon, ChancesDozing? talking, ChancesDozing? lunch, ChancesDozing? traffic)
        {
            _epworthRepository.Save(new EpworthDbModel{ParticipantID = participantID, Reading = reading, TV = tv, PublicPlace = publicplace, PassengerCar = passengercar, Afternoon = afternoon, Talking = talking, Lunch = lunch, Traffic = traffic});
            return View(new DebriefIndexViewModel(participantID, _config.Researchers, _config.DebriefHtml));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
