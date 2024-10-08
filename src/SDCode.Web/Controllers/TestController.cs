using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Options;
using SDCode.Web.Models.Data;
using Microsoft.AspNetCore.Http;

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
        private readonly IConfig _config;
        private readonly ITestResponsesRepository _testResponsesRepository;
        private readonly ISessionMetaRepository _sessionMetaRepository;
        private readonly ICommaDelimitedIntegersCollector _commaDelimitedIntegersCollector;
        private readonly IStimuliImageUrlGetter _stimuliImageUrlGetter;
        private readonly ITestStartTimeGetter _testStartTimeGetter;
        private readonly IReturningUserPhaseDataGetter _returningUserPhaseDataGetter;
        private readonly IConfidencesDescriptionGetter _confidencesDescriptionGetter;
        private readonly IJudgementsDescriptionGetter _judgementsDescriptionGetter;
        private readonly IConfidencesDescriptionsGetter _confidencesDescriptionsGetter;
        private readonly ITestInstructionsViewModelGetter _testInstructionsViewModelGetter;
        private readonly ISleepQuestionsRepository _sleepQuestionsRepository;
        private readonly IObscuredImagesRepository _obscuredImagesRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestController(ILogger<TestController> logger, IPhaseSetsGetter phaseSetsGetter, INextImageGetter nextImageGetter, IImageCongruencyGetter imageCongruencyGetter, ITestNameGetter testNameGetter, IImageContextGetter imageContextGetter, IProgressGetter progressGetter, IStanfordRepository stanfordRepository, IResponseFeedbackGetter responseFeedbackGetter, IOptions<Config> config, ITestResponsesRepository testResponsesRepository, ISessionMetaRepository sessionMetaRepository, ICommaDelimitedIntegersCollector commaDelimitedIntegersCollector, IStimuliImageUrlGetter stimuliImageUrlGetter, ITestStartTimeGetter testStartTimeGetter, IReturningUserPhaseDataGetter returningUserPhaseDataGetter, IConfidencesDescriptionGetter confidencesDescriptionGetter, IJudgementsDescriptionGetter judgementsDescriptionGetter, IConfidencesDescriptionsGetter confidencesDescriptionsGetter, ITestInstructionsViewModelGetter testInstructionsViewModelGetter, ISleepQuestionsRepository sleepQuestionsRepository, IObscuredImagesRepository obscuredImagesRepository, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
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
            _confidencesDescriptionGetter = confidencesDescriptionGetter;
            _judgementsDescriptionGetter = judgementsDescriptionGetter;
            _confidencesDescriptionsGetter = confidencesDescriptionsGetter;
            _testInstructionsViewModelGetter = testInstructionsViewModelGetter;
            _sleepQuestionsRepository = sleepQuestionsRepository;
            _obscuredImagesRepository = obscuredImagesRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult Welcome(string participantID)
        {
            var testInstructionsViewModel = _testInstructionsViewModelGetter.Get(participantID);
            return View(new TestWelcomeViewModel(participantID, testInstructionsViewModel));
        }

        public IActionResult WelcomeBack(string participantID, Sleepinesses? stanford)
        {
            var testName = _testNameGetter.Get(participantID);
            if (stanford.HasValue) {
                _stanfordRepository.Save(participantID, testName, stanford.Value);
            }
            var testInstructionsViewModel = _testInstructionsViewModelGetter.Get(participantID);
            return View(new TestWelcomeBackViewModel(participantID, testInstructionsViewModel));
        }
       
        [HttpPost]
        public IActionResult Stanford(string participantID, string bed, string wake, string latency, string tst)
        {
            var testName = _testNameGetter.Get(participantID);
            _sleepQuestionsRepository.Save(participantID, testName, bed, wake, latency, tst);
            return View(new StanfordViewModel(participantID, false));
        }

        [HttpPost]
        public IActionResult Wait(string participantID, string whenToReturn) {
            return View(new TestWaitViewModel(participantID, whenToReturn));
        }

        [HttpPost]
        public IActionResult Expired() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string participantID, Sleepinesses? stanford)
        {
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(participantID);
            if (stanford.HasValue) {
                _stanfordRepository.Save(participantID, testName, stanford.Value);
            }
            var testAllImageTypes = PhaseSetsGetter.TestOldImageTypes.Union(PhaseSetsGetter.TestNewImageTypes);
            var testInstructionsViewModel = _testInstructionsViewModelGetter.Get(participantID);
            var sessionID = Guid.NewGuid();
            var viewModel = new TestIndexViewModel(participantID, progress, testName, sessionID, _config.AttentionResetDisplayDurationInMilliseconds, _config.AutomateTests, _config.TestAutomationDelayInMilliseconds, testAllImageTypes, testInstructionsViewModel, _config.ImageTypesImageUrlTemplate, _config.ImageTypesAudioUrlTemplate);
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
        public IActionResult ResponseData(string participantID, Guid sessionID, int progress, Judgements judgement, Confidences confidence, long reactionTime) {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var seenTestName = _testNameGetter.Get(phaseSets, progress);
            var seenViewModel = GetViewModel(phaseSets, progress);
            var congruency = _imageCongruencyGetter.Get(seenViewModel.ImageToDisplay);
            var context = _imageContextGetter.Get(seenViewModel.ImageToDisplay);
            var feedback = _responseFeedbackGetter.Get(seenViewModel.ImageToDisplay, judgement);
            var imageResponse = new ResponseDbDataModel{ParticipantID = participantID, TestName = seenTestName, SessionID = sessionID, Image = seenViewModel.ImageToDisplay, Congruency = congruency, Context = context, Judgement = judgement, Confidence = confidence, ReactionTime = reactionTime, Feedback = feedback, WhenUtc = DateTime.UtcNow};
            _testResponsesRepository.Save(imageResponse);
            var nextProgress = progress + 1;
            var nextTestName = _testNameGetter.Get(phaseSets, nextProgress);
            var testHasEnded = !string.Equals(seenTestName, nextTestName);
            var nextViewModel = testHasEnded ? null : GetViewModel(phaseSets, nextProgress);
            return Json(new {TestEnded=testHasEnded, feedback=((int)feedback), ViewModel=nextViewModel});
        }

        private TestImageViewModel GetViewModel(PhaseSets phaseSets, int progress) {
            var imageToDisplay = _nextImageGetter.Get(phaseSets, progress);
            var result = new TestImageViewModel(phaseSets.ParticipantID, progress, imageToDisplay);
            return result;
        }

        [HttpPost]
        public IActionResult Questions(string participantID, string testName, string obscuredIndexesCommaDelimited)
        {
            var obscuredIndexes = _commaDelimitedIntegersCollector.Collect(obscuredIndexesCommaDelimited);
            var sessionMeta = _sessionMetaRepository.Get(participantID, testName);
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var testImages = ((IEnumerable<string>)(phaseSets.GetType().GetProperty(testName) ?? throw new Exception("Unexpected phase name.")).GetValue(phaseSets)).ToList();
            var obscuredImages = obscuredIndexes.Select(x=>testImages[x]);
            _obscuredImagesRepository.Save(participantID, testName, obscuredImages);
            _sessionMetaRepository.Save(sessionMeta);
            return View(new TestQuestionsViewModel(participantID, testName));
        }

        [HttpPost]
        public IActionResult End(string participantID, string testName, string obscuredReason) {
            var participantRecord = _sessionMetaRepository.Get(participantID, testName);
            participantRecord.ObscuredReason = obscuredReason;
            participantRecord.FinishedWhenUtc = DateTime.UtcNow;
            _sessionMetaRepository.Save(participantRecord);
            SendTestEndNotifications(testName, participantID);
            return View(new TestEndedViewModel(participantID, testName, _config.Researchers));
        }

        private void SendTestEndNotifications(string testName, string participantID)
        {
            if (_config.NotificationsEnabled) {
                try {
                    _emailSender.Send(new EmailAddress(_config.NotificationsFromAddress, _config.NotificationsFromName), new EmailAddress(_config.NotificationsToAddress, _config.NotificationsToName), $"{participantID} {testName}", $"Participant {participantID} completed {testName} ({_httpContextAccessor.HttpContext.Request.Host}).");
                }
                catch (System.Exception exception) {
                    _logger.LogError(exception, $"Unable to send {testName} email ({participantID}).");
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
