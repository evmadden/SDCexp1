using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface IConsentRepository
    {
        ConsentDbModel Get(string participantID);
        void Save(ConsentDbModel consent);
    }

    public class ConsentRepository : IConsentRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public ConsentRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ConsentDbModel Get(string participantID)
        {
            var result = _dbContext.Consents.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new ConsentDbModel{ParticipantID=participantID};
            return result;
        }

        public void Save(ConsentDbModel consent)
        {
            if (_dbContext.Consents.Any(x=>string.Equals(consent.ParticipantID, x.ParticipantID))) {
                _dbContext.Update(consent);
            } else {
                _dbContext.Add(consent);
            }
            _dbContext.SaveChanges();
        }
    }
}
