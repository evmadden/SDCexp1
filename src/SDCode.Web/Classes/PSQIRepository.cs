using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface IPSQIRepository
    {
        PSQIDbModel Get(string participantID);
        void Save(PSQIDbModel psqi);
    }

    public class PSQIRepository : IPSQIRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public PSQIRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PSQIDbModel Get(string participantID)
        {
            var result = _dbContext.PSQIs.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new PSQIDbModel{ParticipantID=participantID};
            return result;
        }

        public void Save(PSQIDbModel psqi)
        {
            if (_dbContext.PSQIs.Any(x=>string.Equals(psqi.ParticipantID, x.ParticipantID))) {
                _dbContext.Update(psqi);
            } else {
                _dbContext.Add(psqi);
            }
            _dbContext.SaveChanges();
        }
    }
}
