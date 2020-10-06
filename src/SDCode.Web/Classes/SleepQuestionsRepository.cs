using System;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ISleepQuestionsRepository
    {
        SleepQuestionsModel Get(string participantID);
        void Save(string participantID, string testName, string bed, string wake, string latency, string tst);
    }

    public class SleepQuestionsRepository : ISleepQuestionsRepository
    {
        private readonly ICsvFile<SleepQuestionsModel, SleepQuestionsModel.Map> _sleepQuestionsCsvFile;

        public SleepQuestionsRepository(ICsvFile<SleepQuestionsModel, SleepQuestionsModel.Map> sleepQuestionsCsvFile)
        {
            _sleepQuestionsCsvFile = sleepQuestionsCsvFile;
        }

        public SleepQuestionsModel Get(string participantID)
        {
            var models = _sleepQuestionsCsvFile.Read().ToList();
            var result = models.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new SleepQuestionsModel{ParticipantID=participantID};
            return result;
        }

        public void Save(string participantID, string testName, string bed, string wake, string latency, string tst)
        {
            var models = _sleepQuestionsCsvFile.Read().ToList();
            var model = models.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new SleepQuestionsModel{ParticipantID=participantID};
            models.RemoveAll(x=>string.Equals(x.ParticipantID, participantID));
            var bedProperty = model.GetType().GetProperty($"{testName}Bed") ?? throw new Exception($"Unexpected test name.");
            bedProperty.SetValue(model, bed, null);
            var wakeProperty = model.GetType().GetProperty($"{testName}Wake") ?? throw new Exception($"Unexpected test name.");
            bedProperty.SetValue(model, wake, null);
            var latencyProperty = model.GetType().GetProperty($"{testName}Latency") ?? throw new Exception($"Unexpected test name.");
            bedProperty.SetValue(model, latency, null);
            var tstProperty = model.GetType().GetProperty($"{testName}TST") ?? throw new Exception($"Unexpected test name.");
            bedProperty.SetValue(model, tst, null);
            models.Add(model);
            _sleepQuestionsCsvFile.Write(models);
        }
    }
}
