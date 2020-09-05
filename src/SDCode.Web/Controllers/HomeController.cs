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

        public HomeController(ILogger<HomeController> logger, ICsvFile<ConsentModel, ConsentMap> consentCsvFile, ICsvFile<DemographicsModel, DemographicsMap> demographicsCsvFile)
        {
            _logger = logger;
            _consentCsvFile = consentCsvFile;
            _demographicsCsvFile = demographicsCsvFile;
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

        public IActionResult PSQI(bool sex, string age, string yearStudy, bool handed, bool impairments, bool glasses, string langauge, string bilingual, string currentCountry, string bed, string wake, string latency, string tst)
        {
            var demographicsModels = new List<DemographicsModel>();
            var demographicsModel = new DemographicsModel{Sex = sex, Age = age, YearStudy = yearStudy, Handed = handed, Impairments = impairments, Glasses = glasses, Language = langauge, Bilingual = bilingual, CurrentCountry = currentCountry, Bed = bed, Wake = wake, Latency = latency, TST = tst};
            demographicsModels.Add(demographicsModel);
            _demographicsCsvFile.Write(demographicsModels);            
            return View();
        }

        public IActionResult Epworth(string q1, string q5)
        {
            Debug.WriteLine($"q1: {q1}", $"q5: {q5}");
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
