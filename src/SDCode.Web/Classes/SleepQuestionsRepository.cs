using System;
using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface ISleepQuestionsRepository
    {
        SleepQuestionsDbModel Get(string participantID);
        void Save(string participantID, string testName, string bed, string wake, string latency, string tst);
    }

    public class SleepQuestionsRepository : ISleepQuestionsRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public SleepQuestionsRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SleepQuestionsDbModel Get(string participantID)
        {
            var result = _dbContext.SleepQuestions.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new SleepQuestionsDbModel{ParticipantID=participantID};
            return result;
        }

        public void Save(string participantID, string testName, string bed, string wake, string latency, string tst)
        {
            var model = _dbContext.SleepQuestions.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new SleepQuestionsDbModel{ParticipantID=participantID};
            var bedProperty = model.GetType().GetProperty($"{testName}Bed") ?? throw new Exception($"Unexpected test name.");
            bedProperty.SetValue(model, bed, null);
            var wakeProperty = model.GetType().GetProperty($"{testName}Wake") ?? throw new Exception($"Unexpected test name.");
            wakeProperty.SetValue(model, wake, null);
            var latencyProperty = model.GetType().GetProperty($"{testName}Latency") ?? throw new Exception($"Unexpected test name.");
            latencyProperty.SetValue(model, latency, null);
            var tstProperty = model.GetType().GetProperty($"{testName}TST") ?? throw new Exception($"Unexpected test name.");
            tstProperty.SetValue(model, tst, null);
            if (_dbContext.SleepQuestions.Any(x=>string.Equals(participantID, x.ParticipantID))) {
                _dbContext.Update(model);
            } else {
                _dbContext.Add(model);
            }
            _dbContext.SaveChanges();
        }
    }
}
