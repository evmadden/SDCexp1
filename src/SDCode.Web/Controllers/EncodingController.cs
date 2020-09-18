using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Models;
using SDCode.Web.Classes;

namespace SDCode.Web.Controllers
{
    public class EncodingController : Controller
    {
        private readonly ILogger<EncodingController> _logger;
        private readonly IImageIndexesGetter _imageIndexesGetter;
        private readonly IStimuliImageDataUrlGetter _stimuliImageDataUrlGetter;
        private readonly IStanfordRepository _stanfordRepository;

        public EncodingController(ILogger<EncodingController> logger, IImageIndexesGetter encodingPhaseImageIndexesGetter, IStimuliImageDataUrlGetter stimuliImageDataUrlGetter, IStanfordRepository stanfordRepository)
        {
            _logger = logger;
            _imageIndexesGetter = encodingPhaseImageIndexesGetter;
            _stimuliImageDataUrlGetter = stimuliImageDataUrlGetter;
            _stanfordRepository = stanfordRepository;
        }

        public IActionResult Index(string participantID, Sleepinesses stanford)
        {
            _stanfordRepository.Save(participantID, "Immediate", stanford);
            var imageTypes = new List<string> { "A", "A", "AI", "AI", "B", "BI", "C", "CI", "F", "F", "FI", "FI" };
            var imageIndexesRequests = imageTypes.Select(x=>new ImageIndexesRequest(x, 36));
            var imageIndexes = _imageIndexesGetter.Get(imageIndexesRequests);
            var imageUrls = _stimuliImageDataUrlGetter.Get(imageIndexes);
            var viewModel = new EncodingIndexViewModel(participantID, imageUrls, stanford);
            return View(viewModel);
        }

        // todo mlh remove outOfBoundsImageIndex if we're not going to use it
        public IActionResult ImageOutOfBounds(string participantID, Sleepinesses stanford, int outOfBoundsImageIndex) {
            var viewModel = new EncodingImageOutOfBoundsViewModel(participantID, stanford);
            return View(viewModel);
        }

        public IActionResult Finished(string participantID, string neglectedIndexesCommaDelimited)
        {
            // todo mlh remove
            // 447_Immediate.csv  testResponseData
            // congruency (1 - congruent, 2 - incongruent), context (1 - no change, 2 - still in context, 3 - decontextualized, 4 - foil), old/new judgment, reaction time, confidence rating
            // congruency:  1 = "x"    2 = "_I"
            // context:  1 = "F" "FI"   2 = "A" "AI" "E" "EI"   3 = B-D & BI-DI   4 = "N" "NI"
            // old/new judgment: record what they pressed (maybe coded as 1 or 2... maybe otherwise coded as somehow representing what they actually pressed on the keyboard)
            // reaction time: ms from image display to user response
            // confidence rating: 1 not confident and 4 very confident

            // tests show E and EI (instead of A/AI) + D and DI (instead of B/BI/C/CI) + F and FI + N and NI (SINGLE sets all)
            // 288 TOTAL IMAGES per session

            // immediate delayed followup

            var neglectedIndexes = neglectedIndexesCommaDelimited?.Split(",").Select(int.Parse) ?? new List<int>();
            return View(new EncodingFinishedViewModel(participantID, neglectedIndexes));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
