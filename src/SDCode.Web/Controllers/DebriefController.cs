using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Classes;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class DebriefController : Controller
    {
        private readonly ILogger<DebriefController> _logger;
        private readonly IRepository<EpworthModel> _epworthRepository;

        public DebriefController(ILogger<DebriefController> logger, IRepository<EpworthModel> epworthRepository)
        {
            _logger = logger;
            _epworthRepository = epworthRepository;
        }

        [HttpPost]
        public IActionResult Index(string participantID, ChancesDozing? reading, ChancesDozing? tv, ChancesDozing? publicplace, ChancesDozing? passengercar, ChancesDozing? afternoon, ChancesDozing? talking, ChancesDozing? lunch, ChancesDozing? traffic)
        {
            _epworthRepository.Save(new EpworthModel{ParticipantID = participantID, Reading = reading, TV = tv, PublicPlace = publicplace, PassengerCar = passengercar, Afternoon = afternoon, Talking = talking, Lunch = lunch, Traffic = traffic});
            return View(new DebriefIndexViewModel(participantID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
