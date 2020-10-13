using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Classes;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly SQLiteDBContext _dbContext;

        public AdminController(ILogger<AdminController> logger, SQLiteDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string pID, string password)
        {
            IDictionary<string, object> result;
            if (string.Equals(password, Environment.GetEnvironmentVariable("PASSWORD_ADMIN"))) {
                var participantID = pID;
                var entities = new List<object>{
                    _dbContext.Consents.Find(participantID),
                    _dbContext.Demographics.Find(participantID),
                    _dbContext.PSQIs.Find(participantID),
                    _dbContext.Epworths.Find(participantID),
                    _dbContext.Stanfords.Find(participantID),
                    _dbContext.SleepQuestions.Find(participantID),
                };
                entities.AddRange(_dbContext.PhaseImages.Where(x=>string.Equals(participantID, x.ParticipantID)));
                entities.AddRange(_dbContext.SessionMetas.Where(x=>string.Equals(participantID, x.ParticipantID)));
                entities.AddRange(_dbContext.ResponseDatas.Where(x=>string.Equals(participantID, x.ParticipantID)));
                entities = entities.Where(x=>x!=null).ToList();
                entities.ForEach(x=>_dbContext.Remove(x));
                _dbContext.SaveChanges();
                result = new Dictionary<string, object>{{"success",true},{"message", $"{entities.Count} records removed."}};
            } else {
                result = new Dictionary<string, object>{{"success",false},{"errorMessage", "Password incorrect."}};
            }
            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
