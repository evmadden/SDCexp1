using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IStanfordRepository
    {
        StanfordModel Get(string participantID, string testName);
        void Save(string participantID, string testName, string stanford);
    }

    public class StanfordRepository : IStanfordRepository
    {
        private readonly ICsvFile<StanfordModel, StanfordMap> _stanfordCsvFile;

        public StanfordRepository(ICsvFile<StanfordModel, StanfordMap> stanfordCsvFile)
        {
            _stanfordCsvFile = stanfordCsvFile;
        }

        public StanfordModel Get(string participantID, string testName)
        {
            var stanfordModels = _stanfordCsvFile.Read().ToList();
            var result = stanfordModels.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new StanfordModel{ParticipantID=participantID};
            return result;
        }

        public void Save(string participantID, string testName, string stanford)
        {
            var stanfordModels = _stanfordCsvFile.Read().ToList();
            var stanfordModel = stanfordModels.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new StanfordModel{ParticipantID=participantID};
            PropertyInfo propertyInfo = stanfordModel.GetType().GetProperty(testName);
            propertyInfo.SetValue(stanfordModel, Convert.ChangeType(stanford, propertyInfo.PropertyType), null);
            _stanfordCsvFile.Write(stanfordModels);
        }
    }
}
