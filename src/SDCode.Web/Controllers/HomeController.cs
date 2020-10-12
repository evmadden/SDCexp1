using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly IConsentRepository _consentRepository;
        private readonly IDemographicsRepository _demographicsRepository;
        private readonly IPSQIRepository _psqiRepository;
        private readonly IEpworthRepository _epworthRepository;
        private readonly ITestNameGetter _testNameGetter;
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly IProgressGetter _progressGetter;
        private readonly IEncodingFinishedChecker _encodingFinishedChecker;
        private readonly IReturningUserPhaseDataGetter _returningUserPhaseDataGetter;
        private readonly IParticipantEnrollmentVerifier _participantEnrollmentVerifier;
        private readonly ISleepQuestionsRepository _sleepQuestionsRepository;

        public HomeController(ILogger<HomeController> logger, IStanfordRepository stanfordRepository, IConsentRepository consentRepository, IPSQIRepository psqiRepository, IEpworthRepository epworthRepository, IDemographicsRepository demographicsRepository, ITestNameGetter testNameGetter, IPhaseSetsGetter phaseSetsGetter, IProgressGetter progressGetter, IEncodingFinishedChecker encodingFinishedChecker, IReturningUserPhaseDataGetter returningUserPhaseDataGetter, IParticipantEnrollmentVerifier participantEnrollmentVerifier, ISleepQuestionsRepository sleepQuestionsRepository)
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
            _sleepQuestionsRepository = sleepQuestionsRepository;
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
        public IActionResult Demographics(string participantID, bool infoSheet, bool withdraw, bool npsDisorder, bool adhd, bool headInjury, bool normalVision, bool visionProblems, bool altShifts, bool dataProtection, bool agreeParticipate)
        {
            _consentRepository.Save(new ConsentDbModel{ParticipantID = participantID, InfoSheet = infoSheet, Withdraw = withdraw, NPSDisorder = npsDisorder, ADHD = adhd, HeadInjury = headInjury, NormalVision = normalVision, VisionProblems = visionProblems, AltShifts = altShifts, DataProtection = dataProtection, AgreeParticipate = agreeParticipate});
            return View(new DemographicsViewModel(participantID));
        }

        public IActionResult PreviouslyInterrupted() {
            return View();
        }

        [HttpPost]
        public IActionResult Epworth(string participantID, string monthbed, string monthlatency, string monthwake, string totalhours, string totalminutes, FrequenciesWeekly? no30min, FrequenciesWeekly? waso, FrequenciesWeekly? bathroom, FrequenciesWeekly? breathing, FrequenciesWeekly? snoring, FrequenciesWeekly? hot, FrequenciesWeekly? cold, FrequenciesWeekly? dreams, FrequenciesWeekly? pain, FrequenciesWeekly? otherfrequency, string otherdescribe, Qualities? sleepquality, FrequenciesWeekly? medication, FrequenciesWeekly? sleepiness, Problems? enthusiasm, BedPartners? bedpartner, FrequenciesWeekly? partsnore, FrequenciesWeekly? breathpause, FrequenciesWeekly? legs, FrequenciesWeekly? disorientation, FrequenciesWeekly? otherrestless, string otherrestdescribe)
        {
            _psqiRepository.Save(new PSQIDbModel{ParticipantID = participantID, MonthBed = monthbed, MonthLatency = monthlatency, MonthWake = monthwake, TotalHours = totalhours, TotalMinutes = totalminutes, No30Min = no30min, WASO = waso, Bathroom = bathroom, Breathing = breathing, Snoring = snoring, Hot = hot, Cold = cold, Dreams = dreams, Pain = pain, OtherFrequency = otherfrequency, OtherDescribe = otherdescribe, SleepQuality = sleepquality, Medication = medication, Sleepiness = sleepiness, Enthusiasm = enthusiasm, BedPartner = bedpartner, PartSnore = partsnore, BreathPause = breathpause, Legs = legs, Disorientation = disorientation, OtherRestless = otherrestless, OtherRestDescribe = otherrestdescribe});
            return View(new EpworthViewModel(participantID));
        }

        [HttpPost]
        public IActionResult Stanford(string participantID, Sexes? sex, string age, string yearStudy, Hands? handed, bool? impairments, bool? glasses, string language, string bilingual, string currentCountry, string bed, string wake, string latency, string tst)
        {
            var testName = _testNameGetter.Get(participantID);
            _demographicsRepository.Save(new DemographicsDbModel{ParticipantID=participantID, Sex=sex, Age=age, YearStudy=yearStudy, Handed=handed, Impairments=impairments, Glasses=glasses, Language=language, Bilingual=bilingual, CurrentCountry=currentCountry });
            _sleepQuestionsRepository.Save(participantID, testName, bed, wake, latency, tst);
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
            string nextActionAfterScreenCheck = null;
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
                    var testName = _testNameGetter.Get(participantID);
                    var preTestQuestionsNeeded = !string.Equals(testName, "Immediate");
                    nextActionAfterScreenCheck = preTestQuestionsNeeded ? Url.Action("Index", "SleepQuestions") : Url.Action("WelcomeBack", "Test");
                    action = Url.Action("Index", "ScreenCheck");
                } else {
                    var stanford = _stanfordRepository.Get(participantID);
                    if (stanford.Immediate.HasValue) {
                        action = Url.Action("PreviouslyInterrupted", "Home");
                    } else {
                        nextActionAfterScreenCheck = Url.Action("ConsentInfo", "Home");
                        action = Url.Action("Index", "ScreenCheck");
                    }
                }
            } else {
                action = Url.Action("NotEnrolled", "Participant", new {id=participantID});
            }
            return Json(new {success=true, action, participantID, whenToReturn, nextActionAfterScreenCheck});
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
