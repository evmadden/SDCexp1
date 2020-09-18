using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;

namespace SDCode.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICsvFile<ConsentModel, ConsentMap> _consentCsvFile;
        private readonly ICsvFile<DemographicsModel, DemographicsMap> _demographicsCsvFile;
        private readonly ICsvFile<PSQIModel, PSQIMap> _psqiCsvFile;
        private readonly ICsvFile<EpworthModel, EpworthMap> _epworthCsvFile;
        private readonly ICsvFile<StanfordModel, StanfordMap> _stanfordCsvFile;

        public HomeController(ILogger<HomeController> logger, ICsvFile<ConsentModel, ConsentMap> consentCsvFile, ICsvFile<DemographicsModel, DemographicsMap> demographicsCsvFile, ICsvFile<PSQIModel, PSQIMap> psqiCsvFile, ICsvFile<EpworthModel, EpworthMap> epworthCsvFile, ICsvFile<StanfordModel, StanfordMap> stanfordCsvFile)
        {
            _logger = logger;
            _consentCsvFile = consentCsvFile;
            _demographicsCsvFile = demographicsCsvFile;
            _psqiCsvFile = psqiCsvFile;
            _epworthCsvFile = epworthCsvFile;
            _stanfordCsvFile = stanfordCsvFile;
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
       
        public IActionResult ConsentInfo(string participantID)
        {
            return View(new HomeConsentInfoViewModel(participantID));
        }

        public IActionResult ConsentAgreed(string participantID)
        {
            return View(new HomeConsentAgreedViewModel(participantID));
        }

        public IActionResult Demographics(string participantID, bool infoSheet, bool withdraw, bool npsDisorder, bool adhd, bool headInjury, bool normalVision, bool visionProblems, bool altShifts, bool smoker, bool dataProtection, bool agreeParticipate)
        {
            Debug.WriteLine("infoSheet"); // todo mlh remove unnecessary debug
            Debug.WriteLine(infoSheet); // todo mlh remove unnecessary debug
            var consentModels = new List<ConsentModel>();
            var consentModel = new ConsentModel{ID = participantID, InfoSheet = infoSheet, Withdraw = withdraw, NPSDisorder = npsDisorder, ADHD = adhd, HeadInjury = headInjury, NormalVision = normalVision, VisionProblems = visionProblems, AltShifts = altShifts, Smoker = smoker, DataProtection = dataProtection, AgreeParticipate = agreeParticipate};
            consentModels.Add(consentModel);
            _consentCsvFile.Write(consentModels);
            return View(new DemographicsViewModel(participantID));
        }

        public IActionResult PSQI(string participantID, string sex, string age, string yearStudy, string handed, string impairments, string glasses, string language, string bilingual, string currentCountry, string bed, string wake, string latency, string tst)
        {
            var demographicsModels = new List<DemographicsModel>();
            var demographicsModel = new DemographicsModel{ParticipantID = participantID, Sex = sex, Age = age, YearStudy = yearStudy, Handed = handed, Impairments = impairments, Glasses = glasses, Language = language, Bilingual = bilingual, CurrentCountry = currentCountry, Bed = bed, Wake = wake, Latency = latency, TST = tst};
            demographicsModels.Add(demographicsModel);
            _demographicsCsvFile.Write(demographicsModels);            
            return View(new PSQIViewModel(participantID));
        }

        public IActionResult Epworth(string participantID, string monthbed, string monthlatency, string monthwake, string totalhours, string totalminutes, FrequenciesWeekly no30min, FrequenciesWeekly waso, FrequenciesWeekly bathroom, FrequenciesWeekly breathing, FrequenciesWeekly snoring, FrequenciesWeekly hot, FrequenciesWeekly cold, FrequenciesWeekly dreams, FrequenciesWeekly pain, FrequenciesWeekly otherfrequency, string otherdescribe, Qualities sleepquality, FrequenciesWeekly medication, FrequenciesWeekly sleepiness, Problems enthusiasm, BedPartners bedpartner, FrequenciesWeekly partsnore, FrequenciesWeekly breathpause, FrequenciesWeekly legs, FrequenciesWeekly disorientation, FrequenciesWeekly otherrestless, string otherrestdescribe)
        {
            var psqiModels = new List<PSQIModel>();
            var psqiModel = new PSQIModel{ParticipantID = participantID, MonthBed = monthbed, MonthLatency = monthlatency, MonthWake = monthwake, TotalHours = totalhours, TotalMinutes = totalminutes, No30Min = no30min, WASO = waso, Bathroom = bathroom, Breathing = breathing, Snoring = snoring, Hot = hot, Cold = cold, Dreams = dreams, Pain = pain, OtherFrequency = otherfrequency, OtherDescribe = otherdescribe, SleepQuality = sleepquality, Medication = medication, Sleepiness = sleepiness, Enthusiasm = enthusiasm, BedPartner = bedpartner, PartSnore = partsnore, BreathPause = breathpause, Legs = legs, Disorientation = disorientation, OtherRestless = otherrestless, OtherRestDescribe = otherrestdescribe};
            psqiModels.Add(psqiModel);
            _psqiCsvFile.Write(psqiModels);
            return View(new EpworthViewModel(participantID));
        }

        public IActionResult Stanford(string participantID, ChancesDozing reading, ChancesDozing tv, ChancesDozing publicplace, ChancesDozing passengercar, ChancesDozing afternoon, ChancesDozing talking, ChancesDozing lunch, ChancesDozing traffic)
        {
            var epworthModels = new List<EpworthModel>();
            var epworthModel = new EpworthModel{ParticipantID = participantID, Reading = reading, TV = tv, PublicPlace = publicplace, PassengerCar = passengercar, Afternoon = afternoon, Talking = talking, Lunch = lunch, Traffic = traffic};
            epworthModels.Add(epworthModel);
            _epworthCsvFile.Write(epworthModels);
            return View(new StanfordViewModel(participantID));
        }

        public IActionResult EncodingInstructions(string participantID, Sleepinesses stanford)
        {
            var stanfordModels = new List<StanfordModel>();
            var stanfordModel = new StanfordModel{ParticipantID = participantID, Immediate = stanford};
            stanfordModels.Add(stanfordModel);
            _stanfordCsvFile.Write(stanfordModels);
            return View(new EncodingInstructionsViewModel(participantID));
        }

        public IActionResult Privacy() 
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
