using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;
using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Options;

namespace SDCode.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly INextImageGetter _nextImageGetter;
        private readonly IImageCongruencyGetter _imageCongruencyGetter;
        private readonly ITestNameGetter _testNameGetter;
        private readonly IImageContextGetter _imageContextGetter;
        private readonly IProgressGetter _progressGetter;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly IResponseFeedbackGetter _responseFeedbackGetter;
        private readonly Config _config;

        private readonly ITestResponsesRepository _testResponsesRepository;

        private readonly ISessionMetaRepository _sessionMetaRepository;
        private readonly ICommaDelimitedIntegersCollector _commaDelimitedIntegersCollector;
        private readonly IStimuliImageUrlGetter _stimuliImageUrlGetter;
        private readonly ITestStartTimeGetter _testStartTimeGetter;
        private readonly IReturningUserPhaseDataGetter _returningUserPhaseDataGetter;

        public TestController(ILogger<TestController> logger, IPhaseSetsGetter phaseSetsGetter, INextImageGetter nextImageGetter, IImageCongruencyGetter imageCongruencyGetter, ITestNameGetter testNameGetter, IImageContextGetter imageContextGetter, IProgressGetter progressGetter, IStanfordRepository stanfordRepository, IResponseFeedbackGetter responseFeedbackGetter, IOptions<Config> config, ITestResponsesRepository testResponsesRepository, ISessionMetaRepository sessionMetaRepository, ICommaDelimitedIntegersCollector commaDelimitedIntegersCollector, IStimuliImageUrlGetter stimuliImageUrlGetter, ITestStartTimeGetter testStartTimeGetter, IReturningUserPhaseDataGetter returningUserPhaseDataGetter)
        {
            _logger = logger;
            _phaseSetsGetter = phaseSetsGetter;
            _nextImageGetter = nextImageGetter;
            _imageCongruencyGetter = imageCongruencyGetter;
            _testNameGetter = testNameGetter;
            _imageContextGetter = imageContextGetter;
            _progressGetter = progressGetter;
            _stanfordRepository = stanfordRepository;
            _responseFeedbackGetter = responseFeedbackGetter;
            _config = config.Value;
            _testResponsesRepository = testResponsesRepository;
            _sessionMetaRepository = sessionMetaRepository;
            _commaDelimitedIntegersCollector = commaDelimitedIntegersCollector;
            _stimuliImageUrlGetter = stimuliImageUrlGetter;
            _testStartTimeGetter = testStartTimeGetter;
            _returningUserPhaseDataGetter = returningUserPhaseDataGetter;
        }

        [HttpPost]
        public IActionResult Welcome(string participantID)
        {
            return View(new TestWelcomeViewModel(participantID));
        }

        [HttpPost]
        public IActionResult WelcomeBack(string participantID)
        {
            IReturningUserPhaseData phaseData = _returningUserPhaseDataGetter.Get(participantID);
            if (phaseData.Action == ReturningUserAction.Done) {
                return RedirectToAction("Index", "ThankYou");
            } else {
                return View(new TestWelcomeBackViewModel(participantID, phaseData.Action, phaseData.Progress, phaseData.TestName, phaseData.NextTestWhenUtc));
            }
        }

        [HttpPost]
        public IActionResult Index(string participantID, Sleepinesses? stanford)
        {
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(participantID);
            if (stanford.HasValue) {
                _stanfordRepository.Save(participantID, testName, stanford.Value);
            }
            _testResponsesRepository.Archive(participantID, testName);
            var viewModel = new TestIndexViewModel(participantID, progress, testName, _config.AttentionResetDisplayDurationInMilliseconds, _config.AutomateTests, _config.TestAutomationDelayInMilliseconds);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult GetImage(string participantID, int progress)
        {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var viewModel = GetViewModel(phaseSets, progress);
            return Json(new {viewModel});
        }

        [HttpPost]
        public IActionResult ResponseData(string participantID, int progress, Judgements judgement, Confidences confidence, long reactionTime) {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var seenTestName = _testNameGetter.Get(phaseSets, progress);
            var seenViewModel = GetViewModel(phaseSets, progress);
            var congruency = _imageCongruencyGetter.Get(seenViewModel.ImageUrl);
            var context = _imageContextGetter.Get(seenViewModel.ImageUrl);
            var imageName = Path.GetFileNameWithoutExtension(seenViewModel.ImageUrl);
            var feedback = _responseFeedbackGetter.Get(imageName, judgement);
            var imageResponse = new ResponseDataModel{Image = imageName, Congruency = congruency, Context = context, Judgement = judgement, Confidence = confidence, ReactionTime = reactionTime, Feedback = feedback, WhenUtc = DateTime.UtcNow};
            _testResponsesRepository.Add(participantID, seenTestName, imageResponse);
            var nextProgress = progress + 1;
            var nextTestName = _testNameGetter.Get(phaseSets, nextProgress);
            var testHasEnded = !string.Equals(seenTestName, nextTestName);
            var nextViewModel = testHasEnded ? null : GetViewModel(phaseSets, nextProgress);
            return Json(new {TestEnded=testHasEnded, feedback=((int)feedback), ViewModel=nextViewModel});
        }

        private TestImageViewModel GetViewModel(PhaseSetsModel phaseSets, int progress) {
            var imageToDisplay = _nextImageGetter.Get(phaseSets, progress);
            var imageUrl = _stimuliImageUrlGetter.Get(imageToDisplay);
            var result = new TestImageViewModel(phaseSets.ParticipantID, progress, imageUrl);
            return result;
        }

        [HttpPost]
        public IActionResult Questions(string participantID, string testName, string obscuredIndexesCommaDelimited)
        {
            var obscuredIndexes = _commaDelimitedIntegersCollector.Collect(obscuredIndexesCommaDelimited);
            var sessionMeta = _sessionMetaRepository.Get(participantID, testName);
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var testImages = ((IEnumerable<string>)(phaseSets.GetType().GetProperty(testName) ?? throw new Exception("Unexpected phase name.")).GetValue(phaseSets)).ToList();
            sessionMeta.ObscuredImages = obscuredIndexes.Select(x=>testImages[x]);
            _sessionMetaRepository.Save(sessionMeta);
            return View(new TestQuestionsViewModel(participantID, testName));
        }

        [HttpPost]
        public IActionResult End(string participantID, string testName, string obscuredReason) {
            var participantRecord = _sessionMetaRepository.Get(participantID, testName);
            participantRecord.ObscuredReason = obscuredReason;
            _sessionMetaRepository.Save(participantRecord);
            return View(new TestEndedViewModel(participantID, testName));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
