using Microsoft.AspNetCore.Mvc;

namespace SDCode.Web.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
