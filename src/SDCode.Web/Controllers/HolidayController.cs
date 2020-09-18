// todo mlh remove
// using System;
// using System.Diagnostics;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using SDCode.Web.Classes;
// using SDCode.Web.Models;
// using System.Collections.Generic;

// namespace SDCode.Web.Controllers
// {
//     public class HolidayController : Controller
//     {
//         private readonly ILogger<HolidayController> _logger;
//         private readonly ICsvFile<HolidayModel, HolidayMap> _csvFile;

//         public HolidayController(ILogger<HolidayController> logger, ICsvFile<HolidayModel, HolidayMap> csvFile)
//         {
//             _logger = logger;
//             _csvFile = csvFile;
//         }

//         public IActionResult Index()
//         {
//             return View();
//         }
//         public IActionResult Add(string name, int monthNumber, int dayNumber)
//         {
//             var holidays = new List<HolidayModel>(); // _csvFile.Read().ToList();
//             var holiday = new HolidayModel { Name = name, MonthNumber = monthNumber, DayNumber = dayNumber };
//             holidays.Insert(0, holiday);
//             _csvFile.Write(holidays);
//             return View("Added");
//         }

//         public FileContentResult Download()
//         {
//             var fileDownload = _csvFile.GetDownload();
//             return File(fileDownload.Bytes.ToArray(), fileDownload.ContentType, fileDownload.FileName);
//         }

//         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//         public IActionResult Error()
//         {
//             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//         }
//     }
// }
