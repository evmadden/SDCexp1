using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Classes.Database;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IObscuredImagesRepository
    {
        IEnumerable<string> Get(string participantID, string phaseName);
        void Save(string participantID, string phaseName, IEnumerable<string> indexes);
    }

    public class ObscuredImagesRepository : IObscuredImagesRepository
    {
        private readonly SQLiteDBContext _dbContext;

        public ObscuredImagesRepository(SQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> Get(string participantID, string phaseName)
        {
            var result = _dbContext.ObscuredImages.Where(x=>string.Equals(participantID, x.ParticipantID) && string.Equals(phaseName, x.PhaseName)).Select(x=>x.Index);
            return result;
        }

        public void Save(string participantID, string phaseName, IEnumerable<string> indexes)
        {
            _dbContext.ObscuredImages.RemoveRange(_dbContext.ObscuredImages.Where(x=>string.Equals(participantID, x.ParticipantID) && string.Equals(phaseName, x.PhaseName)));
            _dbContext.AddRange(indexes.Select(x=>new ObscuredImageModel{ParticipantID=participantID,PhaseName=phaseName,Index=x}));
            _dbContext.SaveChanges();
        }
    }
}
