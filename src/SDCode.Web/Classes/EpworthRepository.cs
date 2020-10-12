using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface IEpworthRepository
    {
        EpworthDbModel Get(string participantID);
        void Save(EpworthDbModel epworth);
    }

    public class EpworthRepository : IEpworthRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public EpworthRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EpworthDbModel Get(string participantID)
        {
            var result = _dbContext.Epworths.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new EpworthDbModel{ParticipantID=participantID};
            return result;
        }

        public void Save(EpworthDbModel epworth)
        {
            if (_dbContext.Epworths.Any(x=>string.Equals(epworth.ParticipantID, x.ParticipantID))) {
                _dbContext.Update(epworth);
            } else {
                _dbContext.Add(epworth);
            }
            _dbContext.SaveChanges();
        }
    }
}
