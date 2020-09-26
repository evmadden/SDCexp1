using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class ThankYouController : Controller
    {
        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
