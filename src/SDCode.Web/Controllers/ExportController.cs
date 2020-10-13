using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCode.Web.Classes;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models;
using SDCode.Web.Models.CSV;

namespace SDCode.Web.Controllers
{
    public class ExportController : Controller
    {
        private readonly ILogger<ExportController> _logger;
        private readonly SQLiteDBContext _dbContext;

        public ExportController(ILogger<ExportController> logger, SQLiteDBContext dbContext) {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Authorize(string password) {
            IDictionary<string, object> result;
            if (string.Equals(password, Environment.GetEnvironmentVariable("PASSWORD_EXPORT"))) {
                result = new Dictionary<string, object>{{"success",true}};
            } else {
                result = new Dictionary<string, object>{{"success",false},{"errorMessage", "Password incorrect."}};
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult Download(string password) {
            if (!string.Equals(password, Environment.GetEnvironmentVariable("PASSWORD_EXPORT"))) {
                throw new Exception("Password incorrect.");
            }
            var modelTypeCsvFilenameGetter = new ModelTypeCsvFilenameGetter();
            var consentCsvFile = new CsvFile<ConsentCsvModel, ConsentCsvModel.Map>(modelTypeCsvFilenameGetter);
            var demographicsCsvFile = new CsvFile<DemographicsCsvModel, DemographicsCsvModel.Map>(modelTypeCsvFilenameGetter);
            var psqiCsvFile = new CsvFile<PSQICsvModel, PSQICsvModel.Map>(modelTypeCsvFilenameGetter);
            var epworthCsvFile = new CsvFile<EpworthCsvModel, EpworthCsvModel.Map>(modelTypeCsvFilenameGetter);
            var stanfordCsvFile = new CsvFile<StanfordCsvModel, StanfordCsvModel.Map>(modelTypeCsvFilenameGetter);
            var phaseSetsCsvFile = new CsvFile<PhaseSetsCsvModel, PhaseSetsCsvModel.Map>(modelTypeCsvFilenameGetter);
            var responseDataCsvFile = new CsvFile<ResponseDataCsvModel, ResponseDataCsvModel.Map>(modelTypeCsvFilenameGetter);
            var sessionMetaCsvFile = new CsvFile<SessionMetaCsvModel, SessionMetaCsvModel.Map>(modelTypeCsvFilenameGetter);
            var sleepQuestionsCsvFile = new CsvFile<SleepQuestionsCsvModel, SleepQuestionsCsvModel.Map>(modelTypeCsvFilenameGetter);

            var consentCsvFilePath = consentCsvFile.Write(GetCsvConsents());
            var demographicsCsvFilePath = demographicsCsvFile.Write(GetCsvDemographics());
            var psqiCsvFilePath = psqiCsvFile.Write(GetCsvPsqi());
            var epworthCsvFilePath = epworthCsvFile.Write(GetCsvEpworth());
            var stanfordCsvFilePath = stanfordCsvFile.Write(GetCsvStanford());
            var phaseSetsCsvFilePath = phaseSetsCsvFile.Write(GetCsvPhaseSets());
            var sessionMetaCsvFilePath = sessionMetaCsvFile.Write(GetSessionMetaCsv());
            var sleepQuestionsCsvFilePath = sleepQuestionsCsvFile.Write(GetSleepQuestionsCsv());
            
            var zipFilePaths = new List<string>{
                consentCsvFilePath,
                demographicsCsvFilePath,
                psqiCsvFilePath,
                epworthCsvFilePath,
                stanfordCsvFilePath,
                phaseSetsCsvFilePath,
                sessionMetaCsvFilePath,
                sleepQuestionsCsvFilePath
            };

            var sessionIDs = _dbContext.ResponseDatas.Select(x=>x.SessionID).Distinct();
            foreach (var sessionID in sessionIDs) {
                var sessionResponses = _dbContext.ResponseDatas.Where(x=>Guid.Equals(sessionID, x.SessionID));
                var sessionFirstResponse = sessionResponses.First();
                var participantID = sessionFirstResponse.ParticipantID;
                var testName = sessionFirstResponse.TestName;
                var lastWhenOfThisSession = sessionResponses.Select(x=>x.WhenUtc).Max();
                var lastWhenOfAllSessionsOfThisTest = _dbContext.ResponseDatas.Where(x=>string.Equals(participantID, x.ParticipantID) && string.Equals(testName, x.TestName)).Select(x=>x.WhenUtc).Max();
                var responseData = sessionResponses.OrderBy(x=>x.WhenUtc).First();
                var filename = $"{responseData.ParticipantID}_{responseData.TestName}{(DateTime.Equals(lastWhenOfThisSession, lastWhenOfAllSessionsOfThisTest) ? string.Empty : $"_{responseData.WhenUtc.ToString("yyyyMMddHHmmss")}")}";
                var responseDataCsvFilePath = responseDataCsvFile.WithFilename(filename).Write(GetCsvResponseData(sessionID));
                zipFilePaths.Add(responseDataCsvFilePath);
            }
            var zipFiles = zipFilePaths.Select(x=>(x, System.IO.File.ReadAllBytes(x))).ToList();

            var zipArchive = GetZipArchive(zipFiles);

            zipFilePaths.ForEach(System.IO.File.Delete);

            var content = new System.IO.MemoryStream(zipArchive);
            var contentType = "APPLICATION/octet-stream";
            var fileName = $"MemoryStudy_{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip";
            return File(content, contentType, fileName);
        }

        public static byte[] GetZipArchive(List<(string FileName, byte[] Content)> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(System.IO.Path.GetFileName(file.FileName), CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }
                archiveFile = archiveStream.ToArray();
            }
            return archiveFile;
        }

        private IEnumerable<ResponseDataCsvModel> GetCsvResponseData(Guid sessionID)
        {
            return _dbContext.ResponseDatas.Where(x=>Guid.Equals(sessionID, x.SessionID)).OrderBy(x=>x.WhenUtc).Select(x => new ResponseDataCsvModel
            {
                Confidence = x.Confidence,
                Congruency = x.Congruency,
                Context = x.Context,
                Feedback = x.Feedback,
                Image = x.Image,
                Judgement = x.Judgement,
                ReactionTime = x.ReactionTime,
                WhenUtc = x.WhenUtc                
            });
        }

        private IEnumerable<SleepQuestionsCsvModel> GetSleepQuestionsCsv()
        {
            return _dbContext.SleepQuestions.OrderBy(x=>x.ParticipantID).Select(x => new SleepQuestionsCsvModel
            {
                ParticipantID = x.ParticipantID,
                DelayedBed = x.DelayedBed,
                DelayedLatency = x.DelayedLatency,
                DelayedTST = x.DelayedTST,
                DelayedWake = x.DelayedWake,
                FollowupBed = x.FollowupBed,
                FollowupLatency = x.FollowupLatency,
                FollowupTST = x.FollowupTST,
                FollowupWake = x.FollowupWake,
                ImmediateBed = x.ImmediateBed,
                ImmediateLatency = x.ImmediateLatency,
                ImmediateTST = x.ImmediateTST,
                ImmediateWake = x.ImmediateWake
            });
        }

        private IEnumerable<SessionMetaCsvModel> GetSessionMetaCsv()
        {
            return _dbContext.SessionMetas.OrderBy(x=>x.ParticipantID).Select(x => new SessionMetaCsvModel
            {
                ParticipantID = x.ParticipantID,
                SessionName = x.SessionName,
                FinishedWhenUtc = x.FinishedWhenUtc,
                NeglectedImages = _dbContext.NeglectedImages.Where(y=>string.Equals(x.ParticipantID, y.ParticipantID) && string.Equals(x.SessionName, y.PhaseName)).Select(x=>x.Index),
                NeglectedReason = x.NeglectedReason,
                ObscuredImages = _dbContext.ObscuredImages.Where(y=>string.Equals(x.ParticipantID, y.ParticipantID) && string.Equals(x.SessionName, y.PhaseName)).Select(x=>x.Index),
                ObscuredReason = x.ObscuredReason
            });
        }

        private IEnumerable<PhaseSetsCsvModel> GetCsvPhaseSets()
        {
            var result = new List<PhaseSetsCsvModel>();
            var participantIDs = _dbContext.PhaseImages.OrderBy(x=>x.ParticipantID).Select(x=>x.ParticipantID).Distinct();
            foreach (var participantID in participantIDs) {
                var participantImages = _dbContext.PhaseImages.Where(x=>string.Equals(participantID, x.ParticipantID));
                var encoding = participantImages.Where(x=>string.Equals(x.PhaseName, "Encoding")).Select(x=>x.Index);
                var immediate = participantImages.Where(x=>string.Equals(x.PhaseName, "Immediate")).Select(x=>x.Index);
                var delayed = participantImages.Where(x=>string.Equals(x.PhaseName, "Delayed")).Select(x=>x.Index);
                var followup = participantImages.Where(x=>string.Equals(x.PhaseName, "Followup")).Select(x=>x.Index);
                result.Add(new PhaseSetsCsvModel{ParticipantID=participantID, Encoding=encoding, Immediate=immediate,Delayed=delayed,Followup=followup});
            }
            return result;
        }

        private IEnumerable<StanfordCsvModel> GetCsvStanford()
        {
            return _dbContext.Stanfords.OrderBy(x=>x.ParticipantID).Select(x => new StanfordCsvModel
            {
                ParticipantID = x.ParticipantID,
                Delayed = x.Delayed,
                DelayedUtc = x.DelayedUtc,
                Followup = x.Followup,
                FollowupUtc = x.FollowupUtc,
                Immediate = x.Immediate,
                ImmediateUtc = x.ImmediateUtc
            });
        }

        private IEnumerable<EpworthCsvModel> GetCsvEpworth()
        {
            return _dbContext.Epworths.OrderBy(x=>x.ParticipantID).Select(x => new EpworthCsvModel
            {
                ParticipantID = x.ParticipantID,
                Afternoon = x.Afternoon,
                Lunch = x.Lunch,
                PassengerCar = x.PassengerCar,
                PublicPlace = x.PublicPlace,
                Reading = x.Reading,
                Talking = x.Talking,
                Traffic = x.Traffic,
                TV = x.TV
            });
        }

        private IEnumerable<PSQICsvModel> GetCsvPsqi()
        {
            return _dbContext.PSQIs.OrderBy(x=>x.ParticipantID).Select(x => new PSQICsvModel
            {
                ParticipantID = x.ParticipantID,
                Bathroom = x.Bathroom,
                BedPartner = x.BedPartner,
                Breathing = x.Breathing,
                BreathPause = x.BreathPause,
                Cold = x.Cold,
                Disorientation = x.Disorientation,
                Dreams = x.Dreams,
                Enthusiasm = x.Enthusiasm,
                Hot = x.Hot,
                Legs = x.Legs,
                Medication = x.Medication,
                MonthBed = x.MonthBed,
                MonthLatency = x.MonthLatency,
                MonthWake = x.MonthWake,
                No30Min = x.No30Min,
                OtherDescribe = x.OtherDescribe,
                OtherFrequency = x.OtherFrequency,
                OtherRestDescribe = x.OtherRestDescribe,
                OtherRestless = x.OtherRestless,
                Pain = x.Pain,
                PartSnore = x.PartSnore,
                Sleepiness = x.Sleepiness,
                SleepQuality = x.SleepQuality,
                Snoring = x.Snoring,
                TotalHours = x.TotalHours,
                TotalMinutes = x.TotalMinutes,
                WASO = x.WASO
            });
        }

        private IEnumerable<DemographicsCsvModel> GetCsvDemographics()
        {
            return _dbContext.Demographics.OrderBy(x=>x.ParticipantID).Select(x => new DemographicsCsvModel
            {
                ParticipantID = x.ParticipantID,
                Age = x.Age,
                Bilingual = x.Bilingual,
                CurrentCountry = x.CurrentCountry,
                Glasses = x.Glasses,
                Handed = x.Handed,
                Impairments = x.Impairments,
                Language = x.Language,
                Sex = x.Sex,
                YearStudy = x.YearStudy
            });
        }

        private IQueryable<ConsentCsvModel> GetCsvConsents()
        {
            return _dbContext.Consents.OrderBy(x=>x.ParticipantID).Select(x => new ConsentCsvModel
            {
                ParticipantID = x.ParticipantID,
                InfoSheet = x.InfoSheet,
                Withdraw = x.Withdraw,
                NPSDisorder = x.NPSDisorder,
                ADHD = x.ADHD,
                HeadInjury = x.HeadInjury,
                NormalVision = x.NormalVision,
                VisionProblems = x.VisionProblems,
                AltShifts = x.AltShifts,
                DataProtection = x.DataProtection,
                AgreeParticipate = x.AgreeParticipate
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
