using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;
using System.IO;
using System.Collections.Generic;
using System;

namespace SDCode.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IImageIndexesGetter _imageIndexesGetter;
        private readonly IStimuliImageUrlGetter _stimuliImageUrlGetter;
        private readonly ICsvFile<TestSetsModel, TestSetsModel.Map> _testSetsCsvFile;
        private readonly ICsvFile<ResponseDataModel, ResponseDataModel.Map> _responseDataCsvFile;
        private readonly ITestSetsGetter _testSetsGetter;
        private readonly INextImageGetter _nextImageGetter;
        private readonly IImageCongruencyGetter _imageCongruencyGetter;
        private readonly ITestNameGetter _testNameGetter;
        private readonly IImageContextGetter _imageContextGetter;
        private readonly IProgressGetter _progressGetter;
        private readonly ICsvFile<StanfordModel, StanfordMap> _stanfordCsvFile;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly IResponseFeedbackGetter _responseFeedbackGetter;
        private readonly ICsvFile<NeglectedModel, NeglectedModel.Map> _neglectedCsvFile;


        public TestController(ILogger<TestController> logger, IImageIndexesGetter imageIndexesGetter, IStimuliImageUrlGetter stimuliImageUrlGetter, ICsvFile<TestSetsModel, TestSetsModel.Map> testSetsCsvFile, ITestSetsGetter testSetsGetter, INextImageGetter nextImageGetter, IImageCongruencyGetter imageCongruencyGetter, ICsvFile<ResponseDataModel, ResponseDataModel.Map> responseDataCsvFile, ITestNameGetter testNameGetter, IImageContextGetter imageContextGetter, IProgressGetter progressGetter, ICsvFile<StanfordModel, StanfordMap> stanfordCsvFile, IStanfordRepository stanfordRepository, IResponseFeedbackGetter responseFeedbackGetter, ICsvFile<NeglectedModel, NeglectedModel.Map> neglectedCsvFile)
        {
            _logger = logger;
            _imageIndexesGetter = imageIndexesGetter;
            _stimuliImageUrlGetter = stimuliImageUrlGetter;
            _testSetsCsvFile = testSetsCsvFile;
            _testSetsGetter = testSetsGetter;
            _nextImageGetter = nextImageGetter;
            _imageCongruencyGetter = imageCongruencyGetter;
            _responseDataCsvFile = responseDataCsvFile;
            _testNameGetter = testNameGetter;
            _imageContextGetter = imageContextGetter;
            _progressGetter = progressGetter;
            _stanfordCsvFile = stanfordCsvFile;
            _stanfordRepository = stanfordRepository;
            _responseFeedbackGetter = responseFeedbackGetter;
            _neglectedCsvFile = neglectedCsvFile;
        }

        [HttpPost]
        public IActionResult WelcomeBack(string participantID)
        {
            var testSets = _testSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(testSets, progress);
            TestWelcomeBackAction action;
            DateTime? nextTestWhenUtc = default;
            if (string.Equals(testName, nameof(testSets.Immediate))) {
                var stanford = _stanfordRepository.Get(participantID, testName);
                if (string.IsNullOrWhiteSpace(stanford.Immediate)) {
                    action = TestWelcomeBackAction.NewUser;
                } else {
                    action = TestWelcomeBackAction.Test;
                }
            } else {
                action = TestWelcomeBackAction.Stanford;
                var testStartDelayHourThresholds = new Dictionary<string, TimeSpan>() {{nameof(testSets.Delayed), Timespans.OneDay}, {nameof(testSets.Followup), Timespans.OneWeek}};
                if (testStartDelayHourThresholds.ContainsKey(testName)) {
                    var priorTestName = _testNameGetter.Get(testSets, progress-1);
                    var priorTestResponsesCsvFile = GetResponseDataCsvFile(participantID, priorTestName);
                    var priorTestResponses = priorTestResponsesCsvFile.Read();
                    var priorTestStartTimeUtc = priorTestResponses.Select(x=>x.WhenUtc).DefaultIfEmpty().Min();
                    var nextTestStartTimeThresholdUtc = priorTestStartTimeUtc + testStartDelayHourThresholds[testName];
                    if (nextTestStartTimeThresholdUtc > DateTime.UtcNow) {
                        action = TestWelcomeBackAction.Wait;
                        nextTestWhenUtc = nextTestStartTimeThresholdUtc;
                    }
                }
            }
            return View(new TestWelcomeBackViewModel(participantID, action, progress, testName, nextTestWhenUtc));
        }

        [HttpPost]
        public IActionResult Index(string participantID, string stanford, string neglectedIndexesCommaDelimited, string neglectedReason)
        {
            var neglectedIndexes = neglectedIndexesCommaDelimited?.Split(",").Select(int.Parse) ?? new List<int>();
            if (neglectedIndexes.Any()) {
                var neglectedRecords = _neglectedCsvFile.Read().ToList();
                neglectedRecords.Add(new NeglectedModel{ParticipantID=participantID,Indexes=neglectedIndexes,Reason=neglectedReason});
                _neglectedCsvFile.Write(neglectedRecords);
            }
            var testSets = _testSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(testSets, progress);
            if (stanford != default && stanford != "skip") {
                _stanfordRepository.Save(participantID, testName, stanford);
            }
            var viewModel = new TestIndexViewModel(participantID, progress, testName);
            return View(viewModel);
        }

        // todo mlh remove
        // public IActionResult Answers(string participantID) {
        //     return RedirectToAction("Index", "Test");
        // }

        [HttpPost]
        public IActionResult GetImage(string participantID, int progress)
        {
            var testSets = _testSetsGetter.Get(participantID);
            var viewModel = GetViewModel(testSets, progress);
            return Json(new {viewModel});
        }

        [HttpPost]
        public IActionResult ResponseData(string participantID, int progress, Judgements judgement, Confidences confidence, long reactionTime) {
            // todo mlh remove
            // 447_Immediate.csv
            // congruency (1 - congruent, 2 - incongruent), context (1 - no change, 2 - still in context, 3 - decontextualized, 4 - foil)
            // congruency:  1 = "x"    2 = "_I"
            // context:  1 = "F" "FI"   2 = "A" "AI" "E" "EI"   3 = B-D & BI-DI   4 = "N" "NI"
            // old/new judgment: record what they pressed
            // reaction time: ms from image display to user response
            // confidence rating: 1 not confident and 4 very confident
            // tests show E and EI (instead of A/AI) + D and DI (instead of B/BI/C/CI) + F and FI + N and NI (SINGLE sets all - 288 total images per session)
            var testSets = _testSetsGetter.Get(participantID);
            var seenTestName = _testNameGetter.Get(testSets, progress);
            var seenViewModel = GetViewModel(testSets, progress);
            var congruency = _imageCongruencyGetter.Get(seenViewModel.ImageUrl);
            var context = _imageContextGetter.Get(seenViewModel.ImageUrl);
            var responses = GetResponseDataCsvFile(participantID, seenTestName).Read().ToList();
            var imageName = Path.GetFileNameWithoutExtension(seenViewModel.ImageUrl);
            var feedback = _responseFeedbackGetter.GetJudgementIsCorrect(imageName, judgement);
            responses.Insert(0, new ResponseDataModel{Image = imageName, Congruency = congruency, Context = context, Judgement = judgement, Confidence = confidence, ReactionTime = reactionTime, Feedback = feedback, WhenUtc = DateTime.UtcNow});
            GetResponseDataCsvFile(participantID, seenTestName).Write(responses);
            int nextProgress = progress + 1;
            var nextTestName = _testNameGetter.Get(testSets, nextProgress);
            var testHasEnded = !string.Equals(seenTestName, nextTestName);
            var nextViewModel = GetViewModel(testSets, nextProgress);
            return Json(new {TestEnded=testHasEnded ? seenTestName : null, feedback=feedback, ViewModel=nextViewModel});
        }

        private ICsvFile<ResponseDataModel, ResponseDataModel.Map> GetResponseDataCsvFile(string participantID, string testName) {
            var csvFilename = $"{participantID}_{testName}";
            var result = _responseDataCsvFile.WithFilename(csvFilename);
            return result;
        }

        private TestImageViewModel GetViewModel(TestSetsModel testSets, int progress) {
            var imageToDisplay = _nextImageGetter.Get(testSets, progress);
            var imageUrl = _stimuliImageUrlGetter.Get(imageToDisplay);
            var result = new TestImageViewModel(testSets.ParticipantID, progress, imageUrl);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
