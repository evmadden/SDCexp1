using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface INeglectedImagesRepository
    {
        IEnumerable<string> Get(string participantID, string phaseName);
        void Save(string participantID, string phaseName, IEnumerable<string> indexes);
    }

    public class NeglectedImagesRepository : INeglectedImagesRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public NeglectedImagesRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> Get(string participantID, string phaseName)
        {
            var result = _dbContext.NeglectedImages.Where(x=>string.Equals(participantID, x.ParticipantID) && string.Equals(phaseName, x.PhaseName)).Select(x=>x.Index);
            return result;
        }

        public void Save(string participantID, string phaseName, IEnumerable<string> indexes)
        {
            _dbContext.NeglectedImages.RemoveRange(_dbContext.NeglectedImages.Where(x=>string.Equals(participantID, x.ParticipantID) && string.Equals(phaseName, x.PhaseName)));
            _dbContext.AddRange(indexes.Select(x=>new NeglectedImageModel{ParticipantID=participantID,PhaseName=phaseName,Index=x}));
            _dbContext.SaveChanges();
        }
    }
}
