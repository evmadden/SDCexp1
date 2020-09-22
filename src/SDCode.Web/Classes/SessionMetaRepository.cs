using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ISessionMetaRepository
    {
        SessionMetaModel Get(string participantID, string testName);
        void Save(SessionMetaModel sessionMeta);
    }

    public class SessionMetaRepository : ISessionMetaRepository
    {
        private readonly ICsvFile<SessionMetaModel, SessionMetaModel.Map> _csvFile;

        public SessionMetaRepository(ICsvFile<SessionMetaModel, SessionMetaModel.Map> csvFile)
        {
            _csvFile = csvFile;
        }

        public SessionMetaModel Get(string participantID, string testName)
        {
            var records = _csvFile.Read().ToList();
            var result = records.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.SessionName, testName)) ?? new SessionMetaModel{ParticipantID=participantID, SessionName=testName};
            return result;
        }

        public void Save(SessionMetaModel sessionMeta)
        {
            var records = _csvFile.Read().ToList();
            records.RemoveAll(x => string.Equals(x.ParticipantID, sessionMeta.ParticipantID) && string.Equals(x.SessionName, sessionMeta.SessionName));
            records.Add(sessionMeta);
            _csvFile.Write(records);
        }
    }
}
