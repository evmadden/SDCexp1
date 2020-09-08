using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;

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
        private readonly ICurrentTestNameGetter _currentTestNameGetter;
        private readonly IImageContextGetter _imageContextGetter;

        public TestController(ILogger<TestController> logger, IImageIndexesGetter imageIndexesGetter, IStimuliImageUrlGetter stimuliImageUrlGetter, ICsvFile<TestSetsModel, TestSetsModel.Map> testSetsCsvFile, ITestSetsGetter testSetsGetter, INextImageGetter nextImageGetter, IImageCongruencyGetter imageCongruencyGetter, ICsvFile<ResponseDataModel, ResponseDataModel.Map> responseDataCsvFile, ICurrentTestNameGetter currentTestNameGetter, IImageContextGetter imageContextGetter)
        {
            _logger = logger;
            _imageIndexesGetter = imageIndexesGetter;
            _stimuliImageUrlGetter = stimuliImageUrlGetter;
            _testSetsCsvFile = testSetsCsvFile;
            _testSetsGetter = testSetsGetter;
            _nextImageGetter = nextImageGetter;
            _imageCongruencyGetter = imageCongruencyGetter;
            _responseDataCsvFile = responseDataCsvFile;
            _currentTestNameGetter = currentTestNameGetter;
            _imageContextGetter = imageContextGetter;
        }

        public IActionResult Index(string participantID, int progress)
        {
            var testSets = _testSetsGetter.Get(participantID);
            var viewModel = GetViewModel(testSets, progress);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ResponseData(string participantID, int progress) {
            // 447_Immediate.csv
            // congruency (1 - congruent, 2 - incongruent), context (1 - no change, 2 - still in context, 3 - decontextualized, 4 - foil), old/new judgment, reaction time, confidence rating
            // congruency:  1 = "x"    2 = "_I"
            // context:  1 = "F" "FI"   2 = "A" "AI" "E" "EI"   3 = B-D & BI-DI   4 = "N" "NI"
            // old/new judgment: record what they pressed (maybe coded as 1 or 2... maybe otherwise coded as somehow representing what they actually pressed on the keyboard)
            // reaction time: ms from image display to user response
            // confidence rating: 1 not confident and 4 very confident
            // tests show E and EI (instead of A/AI) + D and DI (instead of B/BI/C/CI) + F and FI + N and NI (SINGLE sets all - 288 total images per session)
            var testSets = _testSetsGetter.Get(participantID);
            var testName = _currentTestNameGetter.Get(testSets, progress);
            var seenViewModel = GetViewModel(testSets, progress);
            var congruency = _imageCongruencyGetter.Get(seenViewModel.ImageUrl);
            var context = _imageContextGetter.Get(seenViewModel.ImageUrl);
            string csvFilename = $"{participantID}_{testName}";
            var responses = _responseDataCsvFile.WithFilename(csvFilename).Read().ToList();
            responses.Insert(0, new ResponseDataModel{Congruency = congruency, Context = context});
            _responseDataCsvFile.WithFilename(csvFilename).Write(responses);
            // todo mlh should ResponseDataModel include some reference to which image it regards?
            // todo mlh what to do when last image of test has been seen?
            var nextViewModel = GetViewModel(testSets, progress+1);
            return Json(nextViewModel);
        }

        private TestImmediateViewModel GetViewModel(TestSetsModel testSets, int progress) {
            var imageToDisplay = _nextImageGetter.Get(testSets, progress);
            var imageUrl = _stimuliImageUrlGetter.Get(imageToDisplay);
            var result = new TestImmediateViewModel(testSets.ParticipantID, progress, imageUrl);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
