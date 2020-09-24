using Microsoft.AspNetCore.Mvc;
using SDCode.Web.Models;

namespace SDCode.Web.Controllers
{
    public class ParticipantController : Controller
    {
        [HttpPost]
        public IActionResult NotEnrolled(string id)
        {
            return View(new ParticipantNotEnrolledViewModel(id));
        }
    }
}
