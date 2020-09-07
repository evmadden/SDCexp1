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
    public class EncodingController : Controller
    {
        private readonly ILogger<EncodingController> _logger;
        private readonly IEncodingPhaseImageIndexesGetter _encodingPhaseImageIndexesGetter;
        private readonly IStimuliImageUrlGetter _stimuliImageUrlGetter;

        public EncodingController(ILogger<EncodingController> logger, IEncodingPhaseImageIndexesGetter encodingPhaseImageIndexesGetter, IStimuliImageUrlGetter stimuliImageUrlGetter)
        {
            _logger = logger;
            _encodingPhaseImageIndexesGetter = encodingPhaseImageIndexesGetter;
            _stimuliImageUrlGetter = stimuliImageUrlGetter;
        }

        public IActionResult Index()
        {
            var imageIndexes = _encodingPhaseImageIndexesGetter.Get();
            var imageUrls = _stimuliImageUrlGetter.Get(imageIndexes);
            var encodingIndexViewModel = new EncodingIndexViewModel(imageUrls);
            return View(encodingIndexViewModel);
        }

        public IActionResult EncodingFinishedMessage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
