using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface ISessionMetaRepository
    {
        SessionMetaDbModel Get(string participantID, string sessionName);
        void Save(SessionMetaDbModel sessionMeta);
    }

    public class SessionMetaRepository : ISessionMetaRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public SessionMetaRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SessionMetaDbModel Get(string participantID, string sessionName)
        {
            var result = _dbContext.SessionMetas.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID) && string.Equals(x.SessionName, sessionName)) ?? new SessionMetaDbModel{ParticipantID=participantID, SessionName=sessionName};
            return result;
        }

        public void Save(SessionMetaDbModel sessionMeta)
        {
            if (_dbContext.SessionMetas.Any(x=>string.Equals(sessionMeta.ParticipantID, x.ParticipantID) && string.Equals(sessionMeta.SessionName, x.SessionName))) {
                _dbContext.Update(sessionMeta);
            } else {
                _dbContext.Add(sessionMeta);
            }
            _dbContext.SaveChanges();
        }
    }
}
