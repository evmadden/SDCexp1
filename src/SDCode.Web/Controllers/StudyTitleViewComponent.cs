using Microsoft.AspNetCore.Mvc;
using SDCode.Web.Classes;
using Microsoft.Extensions.Options;

namespace SDCode.Web.Controllers {
    // https://stackoverflow.com/a/52013931
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    public class StudyTitleViewComponent : ViewComponent {
        private readonly IConfig _config;

        public StudyTitleViewComponent(IOptions<Config> config) {
            _config = config.Value;
        }

        public IViewComponentResult Invoke() {
            return View("Default", _config.StudyTitle); // Views/Shared/Components/StudyTitle/Default.cshtml
        }
    }
}