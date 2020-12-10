using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDCode.Web.Classes;

namespace SDCode.Web.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILogger<LanguageController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageController(ILogger<LanguageController> logger, IEmailSender emailSender, IOptions<Config> config, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _emailSender = emailSender;
            _config = config.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index(string participantID)
        {
            if (_config.NotificationsEnabled) {
                try {
                    _emailSender.Send(new EmailAddress(_config.NotificationsFromAddress, _config.NotificationsFromName), new EmailAddress(_config.NotificationsToAddress, _config.NotificationsToName), $"{participantID} Lang Disqual", $"Participant {participantID} Lang Disqual ({_httpContextAccessor.HttpContext.Request.Host}).");
                }
                catch (System.Exception exception) {
                    _logger.LogError(exception, $"Unable to send Lang Disqual email ({participantID}).");
                }
            }
            return View();
        }
    }
}
