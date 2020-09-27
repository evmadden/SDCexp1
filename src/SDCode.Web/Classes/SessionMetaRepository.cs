using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ISessionMetaRepository
    {
        SessionMetaModel Get(string participantID, string sessionName);
        void Save(SessionMetaModel sessionMeta);
    }

    public class SessionMetaRepository : ISessionMetaRepository
    {
        private readonly ICsvFile<SessionMetaModel, SessionMetaModel.Map> _csvFile;

        public SessionMetaRepository(ICsvFile<SessionMetaModel, SessionMetaModel.Map> csvFile)
        {
            _csvFile = csvFile;
        }

        public SessionMetaModel Get(string participantID, string sessionName)
        {
            var records = _csvFile.Read().ToList();
            var result = records.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.SessionName, sessionName)) ?? new SessionMetaModel{ParticipantID=participantID, SessionName=sessionName};
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
