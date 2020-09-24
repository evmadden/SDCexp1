using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IRepository<TModel> where TModel : IParticipantModel, new()
    {
        TModel Get(string participantID);
        void Save(TModel record);
    }

    public class Repository<TModel, TMap> : IRepository<TModel> where TModel : IParticipantModel, new()
    {
        private readonly ICsvFile<TModel, TMap> _csvFile;

        public Repository(ICsvFile<TModel, TMap> csvFile)
        {
            _csvFile = csvFile;
        }

        public TModel Get(string participantID)
        {
            var records = _csvFile.Read().ToList();
            var result = records.SingleOrDefault(x => string.Equals(x.ParticipantID, participantID)) ?? new TModel { ParticipantID = participantID };
            return result;
        }

        public void Save(TModel record)
        {
            var records = _csvFile.Read().ToList();
            records.RemoveAll(x => string.Equals(x.ParticipantID, record.ParticipantID));
            records.Add(record);
            _csvFile.Write(records);
        }
    }
}
