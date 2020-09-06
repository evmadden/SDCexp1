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

        public HomeController(ILogger<HomeController> logger, ICsvFile<ConsentModel, ConsentMap> consentCsvFile, ICsvFile<DemographicsModel, DemographicsMap> demographicsCsvFile, ICsvFile<PSQIModel, PSQIMap> psqiCsvFile)
        {
            _logger = logger;
            _consentCsvFile = consentCsvFile;
            _demographicsCsvFile = demographicsCsvFile;
            _psqiCsvFile = psqiCsvFile;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConsentInfo()
        {
            return View();
        }

        public IActionResult ConsentAgreed()
        {
            return View();
        }

        public IActionResult Demographics(string participantID, bool infoSheet, bool withdraw, bool npsDisorder, bool adhd, bool headInjury, bool normalVision, bool visionProblems, bool altShifts, bool smoker, bool dataProtection, bool agreeParticipate)
        {
            Debug.WriteLine("infoSheet");
            Debug.WriteLine(infoSheet);
            var consentModels = new List<ConsentModel>();
            var consentModel = new ConsentModel{ParticipantID = participantID, InfoSheet = infoSheet, Withdraw = withdraw, NPSDisorder = npsDisorder, ADHD = adhd, HeadInjury = headInjury, NormalVision = normalVision, VisionProblems = visionProblems, AltShifts = altShifts, Smoker = smoker, DataProtection = dataProtection, AgreeParticipate = agreeParticipate};
            consentModels.Add(consentModel);
            _consentCsvFile.Write(consentModels);
            return View();
        }

       /* public IActionResult Demographics(string consent, string withdraw)
        {
            DemographicsViewModel demographicsViewModel=new DemographicsViewModel();
            demographicsViewModel.Consent=consent;
            return View(demographicsViewModel);
        }*/

        public IActionResult PSQI(string sex, string age, string yearStudy, string handed, string impairments, string glasses, string langauge, string bilingual, string currentCountry, string bed, string wake, string latency, string tst)
        {
            var demographicsModels = new List<DemographicsModel>();
            var demographicsModel = new DemographicsModel{Sex = sex, Age = age, YearStudy = yearStudy, Handed = handed, Impairments = impairments, Glasses = glasses, Language = langauge, Bilingual = bilingual, CurrentCountry = currentCountry, Bed = bed, Wake = wake, Latency = latency, TST = tst};
            demographicsModels.Add(demographicsModel);
            _demographicsCsvFile.Write(demographicsModels);            
            return View();
        }

        public IActionResult Epworth(string monthbed, string monthlatency, string monthwake, string totalhours, string totalminutes, string no30min, string waso, string bathroom, string breathing, string snoring, string hot, string cold, string dreams, string pain, string otherfrequency, string otherdescribe, string sleepquality, string medication, string sleepiness, string enthusiasm, string bedpartner, string partsnore, string breathpause, string legs, string disorientation, string otherrestless, string otherrestdescribe)
        {
            var psqiModels = new List<PSQIModel>();
            var psqiModel = new PSQIModel{MonthBed = monthbed, MonthLatency = monthlatency, MonthWake = monthwake, TotalHours = totalhours, TotalMinutes = totalminutes, No30Min = no30min, WASO = waso, Bathroom = bathroom, Breathing = breathing, Snoring = snoring, Hot = hot, Cold = cold, Dreams = dreams, Pain = pain, OtherFrequency = otherfrequency, OtherDescribe = otherdescribe, SleepQuality = sleepquality, Medication = medication, Sleepiness = sleepiness, Enthusiasm = enthusiasm, BedPartner = bedpartner, PartSnore = partsnore, BreathPause = breathpause, Legs = legs, Disorientation = disorientation, OtherRestless = otherrestless, OtherRestDescribe = otherrestdescribe};
            psqiModels.Add(psqiModel);
            _psqiCsvFile.Write(psqiModels);
            return View();
        }

        public IActionResult Stanford()
        {
            return View();
        }

        public IActionResult Privacy()
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
