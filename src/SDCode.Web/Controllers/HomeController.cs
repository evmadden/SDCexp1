using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;

namespace SDCode.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly IRepository<ConsentModel> _consentRepository;
        private readonly IRepository<DemographicsModel> _demographicsRepository;
        private readonly IRepository<PSQIModel> _psqiRepository;
        private readonly IRepository<EpworthModel> _epworthRepository;
        private readonly ITestNameGetter _testNameGetter;
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly IProgressGetter _progressGetter;

        public HomeController(ILogger<HomeController> logger, IStanfordRepository stanfordRepository, IRepository<ConsentModel> consentRepository, IRepository<PSQIModel> psqiRepository, IRepository<EpworthModel> epworthRepository, IRepository<DemographicsModel> demographicsRepository, ITestNameGetter testNameGetter, IPhaseSetsGetter phaseSetsGetter, IProgressGetter progressGetter)
        {
            _logger = logger;
            _stanfordRepository = stanfordRepository;
            _consentRepository = consentRepository;
            _psqiRepository = psqiRepository;
            _epworthRepository = epworthRepository;
            _demographicsRepository = demographicsRepository;
            _testNameGetter = testNameGetter;
            _phaseSetsGetter = phaseSetsGetter;
            _progressGetter = progressGetter;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string participantId)
        {
            return View(new HomeWelcomeViewModel(participantId));
        }

        public IActionResult WelcomeBack()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult ConsentInfo(string participantID)
        {
            return View(new HomeConsentInfoViewModel(participantID));
        }

        [HttpPost]
        public IActionResult ConsentForm(string participantID)
        {
            return View(new HomeConsentFormViewModel(participantID));
        }

        [HttpPost]
        public IActionResult Demographics(string participantID, bool infoSheet, bool withdraw, bool npsDisorder, bool adhd, bool headInjury, bool normalVision, bool visionProblems, bool altShifts, bool smoker, bool dataProtection, bool agreeParticipate)
        {
            _consentRepository.Save(new ConsentModel{ParticipantID = participantID, InfoSheet = infoSheet, Withdraw = withdraw, NPSDisorder = npsDisorder, ADHD = adhd, HeadInjury = headInjury, NormalVision = normalVision, VisionProblems = visionProblems, AltShifts = altShifts, Smoker = smoker, DataProtection = dataProtection, AgreeParticipate = agreeParticipate});
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(phaseSets, progress);
            var testIsAvailable = string.Equals(testName, nameof(phaseSets.Immediate));
            if (testIsAvailable) {
                return View(new DemographicsViewModel(participantID));
            } else {
                return RedirectToAction("Index", "ThankYou");
            }
        }

        [HttpPost]
        public IActionResult PSQI(string participantID, Sexes? sex, string age, string yearStudy, Hands? handed, bool? impairments, bool? glasses, string language, string bilingual, string currentCountry, string bed, string wake, string latency, string tst)
        {
            _demographicsRepository.Save(new DemographicsModel{ParticipantID = participantID, Sex = sex, Age = age, YearStudy = yearStudy, Handed = handed, Impairments = impairments, Glasses = glasses, Language = language, Bilingual = bilingual, CurrentCountry = currentCountry, Bed = bed, Wake = wake, Latency = latency, TST = tst});
            return View(new PSQIViewModel(participantID));
        }

        [HttpPost]
        public IActionResult Epworth(string participantID, string monthbed, string monthlatency, string monthwake, string totalhours, string totalminutes, FrequenciesWeekly? no30min, FrequenciesWeekly? waso, FrequenciesWeekly? bathroom, FrequenciesWeekly? breathing, FrequenciesWeekly? snoring, FrequenciesWeekly? hot, FrequenciesWeekly? cold, FrequenciesWeekly? dreams, FrequenciesWeekly? pain, FrequenciesWeekly? otherfrequency, string otherdescribe, Qualities? sleepquality, FrequenciesWeekly? medication, FrequenciesWeekly? sleepiness, Problems? enthusiasm, BedPartners? bedpartner, FrequenciesWeekly? partsnore, FrequenciesWeekly? breathpause, FrequenciesWeekly? legs, FrequenciesWeekly? disorientation, FrequenciesWeekly? otherrestless, string otherrestdescribe)
        {
            _psqiRepository.Save(new PSQIModel{ParticipantID = participantID, MonthBed = monthbed, MonthLatency = monthlatency, MonthWake = monthwake, TotalHours = totalhours, TotalMinutes = totalminutes, No30Min = no30min, WASO = waso, Bathroom = bathroom, Breathing = breathing, Snoring = snoring, Hot = hot, Cold = cold, Dreams = dreams, Pain = pain, OtherFrequency = otherfrequency, OtherDescribe = otherdescribe, SleepQuality = sleepquality, Medication = medication, Sleepiness = sleepiness, Enthusiasm = enthusiasm, BedPartner = bedpartner, PartSnore = partsnore, BreathPause = breathpause, Legs = legs, Disorientation = disorientation, OtherRestless = otherrestless, OtherRestDescribe = otherrestdescribe});
            return View(new EpworthViewModel(participantID));
        }

        [HttpPost]
        public IActionResult Stanford(string participantID, ChancesDozing? reading, ChancesDozing? tv, ChancesDozing? publicplace, ChancesDozing? passengercar, ChancesDozing? afternoon, ChancesDozing? talking, ChancesDozing? lunch, ChancesDozing? traffic)
        {
            _epworthRepository.Save(new EpworthModel{ParticipantID = participantID, Reading = reading, TV = tv, PublicPlace = publicplace, PassengerCar = passengercar, Afternoon = afternoon, Talking = talking, Lunch = lunch, Traffic = traffic});
            return View(new StanfordViewModel(participantID));
        }

        [HttpPost]
        public IActionResult EncodingInstructions(string participantID, Sleepinesses? stanford)
        {
            _stanfordRepository.Save(participantID, "Immediate", stanford);
            return View(new EncodingInstructionsViewModel(participantID));
        }

        public IActionResult Privacy() 
        {
            return View();
        }

        public IActionResult Contact() 
        {
            return View();
        }

        public IActionResult LearnMore() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
