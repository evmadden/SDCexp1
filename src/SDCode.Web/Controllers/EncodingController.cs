using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System;

namespace SDCode.Web.Controllers
{
    public class EncodingController : Controller
    {
        private readonly ILogger<EncodingController> _logger;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly IConfig _config;
        private readonly ISessionMetaRepository _sessionMetaRepository;
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly ICommaDelimitedIntegersCollector _commaDelimitedIntegersCollector;
        private readonly INeglectedImagesRepository _neglectedImagesRepository;
        private readonly IObscuredImagesRepository _obscuredImagesRepository;

        public EncodingController(ILogger<EncodingController> logger, IStanfordRepository stanfordRepository, IOptions<Config> config, ISessionMetaRepository sessionMetaRepository, IPhaseSetsGetter phaseSetsGetter, ICommaDelimitedIntegersCollector commaDelimitedIntegersCollector, INeglectedImagesRepository neglectedImagesRepository, IObscuredImagesRepository obscuredImagesRepository)
        {
            _logger = logger;
            _stanfordRepository = stanfordRepository;
            _config = config.Value;
            _sessionMetaRepository = sessionMetaRepository;
            _phaseSetsGetter = phaseSetsGetter;
            _commaDelimitedIntegersCollector = commaDelimitedIntegersCollector;
            _neglectedImagesRepository = neglectedImagesRepository;
            _obscuredImagesRepository = obscuredImagesRepository;
        }

        [HttpPost]
        public IActionResult Index(string participantID, Sleepinesses? stanford)
        {
            _stanfordRepository.Save(participantID, "Immediate", stanford);
            var viewModel = new EncodingIndexViewModel(participantID, stanford, _config.ImageDisplayDurationInMilliseconds, _config.AttentionResetDisplayDurationInMilliseconds, _config.NumberDisplayProbabilityPercentage, _config.NumberCheckIntervalInMilliseconds, _config.NumberDisplayThresholdInMilliseconds, PhaseSetsGetter.EncodingImageTypes, _config.ImageTypesUrlTemplate);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GetImageDataUrls(string participantID) {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var json = JsonSerializer.Serialize(phaseSets.Encoding);
            Response.Headers.Add("Content-Length", $"{json.Length}");
            return Content(json, "application/json");
        }

        [HttpPost]
        public IActionResult RecordResults(string participantID, string neglectedIndexesCommaDelimited, string obscuredIndexesCommaDelimited)
        {
            var neglectedIndexes = _commaDelimitedIntegersCollector.Collect(neglectedIndexesCommaDelimited);
            var obscuredIndexes = _commaDelimitedIntegersCollector.Collect(obscuredIndexesCommaDelimited);
            var sessionMeta = _sessionMetaRepository.Get(participantID, "Encoding");
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var neglectedImages = neglectedIndexes.Select(x=>phaseSets.Encoding.ElementAt(x)).ToList();
            var obscuredImages = obscuredIndexes.Select(x=>phaseSets.Encoding.ElementAt(x)).ToList();
            sessionMeta.FinishedWhenUtc = DateTime.UtcNow;
            _sessionMetaRepository.Save(sessionMeta);
            _neglectedImagesRepository.Save(participantID, "Encoding", neglectedImages);
            _obscuredImagesRepository.Save(participantID, "Encoding", obscuredImages);
            var nextAction = neglectedImages.Any() || obscuredImages.Any() ? Url.Action(nameof(Questions)) : Url.Action(nameof(Finished));
            return Json(new {success=true, nextAction});
        }

        [HttpPost]
        public IActionResult Questions(string participantID)
        {
            var sessionMeta = _sessionMetaRepository.Get(participantID, "Encoding");
            var neglectedImages = _neglectedImagesRepository.Get(participantID, "Encoding");
            var obscuredImages = _obscuredImagesRepository.Get(participantID, "Encoding");
            return View(new EncodingQuestionsViewModel(participantID, neglectedImages.Any(), obscuredImages.Any()));
        }

        [HttpPost]
        public IActionResult Finished(string participantID, string neglectedReason, string obscuredReason)
        {
            var sessionMeta = _sessionMetaRepository.Get(participantID, "Encoding");
            sessionMeta.NeglectedReason = neglectedReason;
            sessionMeta.ObscuredReason = obscuredReason;
            _sessionMetaRepository.Save(sessionMeta);
            return View(new EncodingFinishedViewModel(participantID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
