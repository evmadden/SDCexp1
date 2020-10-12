using System;
using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface ITestResponsesRepository
    {
        void Save(ResponseDbDataModel model);
        IEnumerable<ResponseDbDataModel> GetResponsesFromMostRecentSession(string participantID, string testName);
    }

    public class TestResponsesRepository : ITestResponsesRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public TestResponsesRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save(ResponseDbDataModel model) {
            _dbContext.Add(model);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ResponseDbDataModel> GetResponsesFromMostRecentSession(string participantID, string testName)
        {
            var whenUtcs = _dbContext.ResponseDatas.Where(x=>string.Equals(participantID, x.ParticipantID) && string.Equals(testName, x.TestName)).Select(x=>x.WhenUtc);
            var latestWhenUtc = whenUtcs.Any() ? whenUtcs.Max() : DateTime.MinValue;
            var sessionID = _dbContext.ResponseDatas.FirstOrDefault(x=>DateTime.Equals(latestWhenUtc, x.WhenUtc))?.SessionID ?? Guid.NewGuid();
            var result = _dbContext.ResponseDatas.Where(x=>Guid.Equals(sessionID, x.SessionID));
            return result;
        }
    }
}
