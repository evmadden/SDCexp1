using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public IActionResult Demographics()
        {
            return View();
        }

       /* public IActionResult Demographics(string consent, string withdraw)
        {
            DemographicsViewModel demographicsViewModel=new DemographicsViewModel();
            demographicsViewModel.Consent=consent;
            return View(demographicsViewModel);
        }*/

        public IActionResult PSQI()
        {
            return View();
        }

        public IActionResult Epworth()
        {
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
