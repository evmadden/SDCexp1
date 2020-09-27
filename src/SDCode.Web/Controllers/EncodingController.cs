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
        private readonly IStimuliImageDataUrlGetter _stimuliImageDataUrlGetter;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly Config _config;
        private readonly ISessionMetaRepository _sessionMetaRepository;
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly ICommaDelimitedIntegersCollector _commaDelimitedIntegersCollector;

        public EncodingController(ILogger<EncodingController> logger, IStimuliImageDataUrlGetter stimuliImageDataUrlGetter, IStanfordRepository stanfordRepository, IOptions<Config> config, ISessionMetaRepository sessionMetaRepository, IPhaseSetsGetter phaseSetsGetter, ICommaDelimitedIntegersCollector commaDelimitedIntegersCollector)
        {
            _logger = logger;
            _stimuliImageDataUrlGetter = stimuliImageDataUrlGetter;
            _stanfordRepository = stanfordRepository;
            _config = config.Value;
            _sessionMetaRepository = sessionMetaRepository;
            _phaseSetsGetter = phaseSetsGetter;
            _commaDelimitedIntegersCollector = commaDelimitedIntegersCollector;
        }

        [HttpPost]
        public IActionResult Index(string participantID, Sleepinesses? stanford)
        {
            _stanfordRepository.Save(participantID, "Immediate", stanford);
            var viewModel = new EncodingIndexViewModel(participantID, stanford, _config.ImageDisplayDurationInMilliseconds, _config.AttentionResetDisplayDurationInMilliseconds, _config.NumberDisplayProbabilityPercentage, _config.NumberCheckIntervalInMilliseconds, _config.NumberDisplayThresholdInMilliseconds);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GetImageDataUrls(string participantID) {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var imageDataUrls = _stimuliImageDataUrlGetter.Get(phaseSets.Encoding);
            var json = JsonSerializer.Serialize(imageDataUrls);
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
            var encoding = phaseSets.Encoding.ToList();
            sessionMeta.NeglectedImages = neglectedIndexes.Select(x=>encoding[x]).ToList();
            sessionMeta.ObscuredImages = obscuredIndexes.Select(x=>encoding[x]).ToList();
            _sessionMetaRepository.Save(sessionMeta);
            var nextAction = sessionMeta.NeglectedImages.Any() || sessionMeta.ObscuredImages.Any() ? Url.Action(nameof(Questions)) : Url.Action(nameof(Finished));
            return Json(new {success=true, nextAction});
        }

        [HttpPost]
        public IActionResult Questions(string participantID)
        {
            var sessionMeta = _sessionMetaRepository.Get(participantID, "Encoding");
            return View(new EncodingQuestionsViewModel(participantID, sessionMeta.NeglectedImages.Any(), sessionMeta.ObscuredImages.Any()));
        }

        [HttpPost]
        public IActionResult Finished(string participantID, string neglectedReason, string obscuredReason)
        {
            var participantRecord = _sessionMetaRepository.Get(participantID, "Encoding");
            participantRecord.NeglectedReason = neglectedReason;
            participantRecord.ObscuredReason = obscuredReason;
            participantRecord.FinishedWhenUtc = DateTime.UtcNow;
            _sessionMetaRepository.Save(participantRecord);
            return View(new EncodingFinishedViewModel(participantID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
