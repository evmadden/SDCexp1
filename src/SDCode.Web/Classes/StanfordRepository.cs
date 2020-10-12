using System;
using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface IStanfordRepository
    {
        StanfordDbModel Get(string participantID);
        void Save(string participantID, string testName, Sleepinesses? stanford);
    }

    public class StanfordRepository : IStanfordRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public StanfordRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StanfordDbModel Get(string participantID)
        {
            var result = _dbContext.Stanfords.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new StanfordDbModel{ParticipantID=participantID};
            return result;
        }

        public void Save(string participantID, string testName, Sleepinesses? stanford)
        {
            var stanfordModel = _dbContext.Stanfords.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new StanfordDbModel{ParticipantID=participantID};
            var testNameProperty = stanfordModel.GetType().GetProperty(testName) ?? throw new Exception($"Unexpected test name.");
            testNameProperty.SetValue(stanfordModel, stanford, null);
            var utcProperty = stanfordModel.GetType().GetProperty($"{testName}Utc") ?? throw new Exception($"Unexpected test name.");
            utcProperty.SetValue(stanfordModel, DateTime.UtcNow, null);
            if (_dbContext.Stanfords.Any(x=>string.Equals(participantID, x.ParticipantID))) {
                _dbContext.Update(stanfordModel);
            } else {
                _dbContext.Add(stanfordModel);
            }
            _dbContext.SaveChanges();
        }
    }
}
