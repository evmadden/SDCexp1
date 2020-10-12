using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes
{
    public interface IDemographicsRepository
    {
        DemographicsDbModel Get(string participantID);
        void Save(DemographicsDbModel demographics);
    }

    public class DemographicsRepository : IDemographicsRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public DemographicsRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DemographicsDbModel Get(string participantID)
        {
            var result = _dbContext.Demographics.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new DemographicsDbModel{ParticipantID=participantID};
            return result;
        }

        public void Save(DemographicsDbModel demographics)
        {
            if (_dbContext.Demographics.Any(x=>string.Equals(demographics.ParticipantID, x.ParticipantID))) {
                _dbContext.Update(demographics);
            } else {
                _dbContext.Add(demographics);
            }
            _dbContext.SaveChanges();
        }
    }
}
