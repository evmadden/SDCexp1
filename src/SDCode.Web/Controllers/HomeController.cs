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
        private readonly IEncodingFinishedChecker _encodingFinishedChecker;
        private readonly IReturningUserPhaseDataGetter _returningUserPhaseDataGetter;
        private readonly IParticipantEnrollmentVerifier _participantEnrollmentVerifier;

        public HomeController(ILogger<HomeController> logger, IStanfordRepository stanfordRepository, IRepository<ConsentModel> consentRepository, IRepository<PSQIModel> psqiRepository, IRepository<EpworthModel> epworthRepository, IRepository<DemographicsModel> demographicsRepository, ITestNameGetter testNameGetter, IPhaseSetsGetter phaseSetsGetter, IProgressGetter progressGetter, IEncodingFinishedChecker encodingFinishedChecker, IReturningUserPhaseDataGetter returningUserPhaseDataGetter, IParticipantEnrollmentVerifier participantEnrollmentVerifier)
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
            _encodingFinishedChecker = encodingFinishedChecker;
            _returningUserPhaseDataGetter = returningUserPhaseDataGetter;
            _participantEnrollmentVerifier = participantEnrollmentVerifier;
        }

        public IActionResult Index()
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
            var encodingFinished = _encodingFinishedChecker.IsFinished(participantID);
            if (encodingFinished) {
                return RedirectToAction("Returning", "Home");
            } else {
                var stanford = _stanfordRepository.Get(participantID);
                if (stanford.Immediate.HasValue) {
                    return RedirectToAction("PreviouslyInterrupted", "Home");
                } else {
                    _consentRepository.Save(new ConsentModel{ParticipantID = participantID, InfoSheet = infoSheet, Withdraw = withdraw, NPSDisorder = npsDisorder, ADHD = adhd, HeadInjury = headInjury, NormalVision = normalVision, VisionProblems = visionProblems, AltShifts = altShifts, Smoker = smoker, DataProtection = dataProtection, AgreeParticipate = agreeParticipate});
                    return View(new DemographicsViewModel(participantID));
                }
            }
        }

        public IActionResult PreviouslyInterrupted() {
            return View();
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
            return View(new StanfordViewModel(participantID, true));
        }

        [HttpPost]
        public IActionResult EncodingInstructions(string participantID, Sleepinesses? stanford, string nextActionAfterImageCheck, bool showSpacebarOrientation)
        {
            _stanfordRepository.Save(participantID, "Immediate", stanford);
            return View(new EncodingInstructionsViewModel(participantID, nextActionAfterImageCheck, showSpacebarOrientation, stanford));
        }

        public IActionResult Privacy() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string participantID) 
        {
            participantID = participantID.Trim();
            var isEnrolled = _participantEnrollmentVerifier.Verify(participantID);
            string action;
            string whenToReturn = null;
            if (isEnrolled) {
                IReturningUserPhaseData phaseData = _returningUserPhaseDataGetter.Get(participantID);
                if (phaseData.Action == ReturningUserAction.Done) {
                    action = Url.Action("Index", "ThankYou");
                } else if (phaseData.Action == ReturningUserAction.Wait) {
                    var nextTestWhenUtc = phaseData.NextTestWhenUtc.Value.AddMinutes(1);
                    whenToReturn = $"{nextTestWhenUtc.ToShortDateString()} {(nextTestWhenUtc.ToString("h:mm tt"))} UTC";
                    action = Url.Action("Wait", "Test");
                } else if (phaseData.Action == ReturningUserAction.TooLate) {
                    action = Url.Action("Expired", "Test");
                } else if (phaseData.Action == ReturningUserAction.Test) {
                    action = Url.Action("WelcomeBack", "Test");
                } else {
                    var stanford = _stanfordRepository.Get(participantID);
                    action = stanford.Immediate.HasValue ? Url.Action("PreviouslyInterrupted", "Home") : Url.Action("ConsentInfo", "Home");
                }
            } else {
                action = Url.Action("NotEnrolled", "Participant", new {id=participantID});
            }
            return Json(new {success=true, action, participantID, whenToReturn});
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
