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

        public EncodingController(ILogger<EncodingController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var imageUrls = new List<string>{"https://placekitten.com/800/700?image=1", "https://placekitten.com/800/700?image=2", "https://placekitten.com/800/700?image=3"};
            var encodingIndexViewModel = new EncodingIndexViewModel(imageUrls);
            return View(encodingIndexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
