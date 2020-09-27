using System;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IStanfordRepository
    {
        StanfordModel Get(string participantID);
        void Save(string participantID, string testName, Sleepinesses? stanford);
    }

    public class StanfordRepository : IStanfordRepository
    {
        private readonly ICsvFile<StanfordModel, StanfordModel.Map> _stanfordCsvFile;

        public StanfordRepository(ICsvFile<StanfordModel, StanfordModel.Map> stanfordCsvFile)
        {
            _stanfordCsvFile = stanfordCsvFile;
        }

        public StanfordModel Get(string participantID)
        {
            var stanfordModels = _stanfordCsvFile.Read().ToList();
            var result = stanfordModels.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new StanfordModel{ParticipantID=participantID};
            return result;
        }

        public void Save(string participantID, string testName, Sleepinesses? stanford)
        {
            var stanfordModels = _stanfordCsvFile.Read().ToList();
            var stanfordModel = stanfordModels.SingleOrDefault(x=>string.Equals(x.ParticipantID, participantID)) ?? new StanfordModel{ParticipantID=participantID};
            stanfordModels.RemoveAll(x=>string.Equals(x.ParticipantID, participantID));
            var testNameProperty = stanfordModel.GetType().GetProperty(testName) ?? throw new Exception($"Unexpected test name.");
            testNameProperty.SetValue(stanfordModel, stanford, null);
            var utcProperty = stanfordModel.GetType().GetProperty($"{testName}Utc") ?? throw new Exception($"Unexpected test name.");
            utcProperty.SetValue(stanfordModel, DateTime.UtcNow, null);
            stanfordModels.Add(stanfordModel);
            _stanfordCsvFile.Write(stanfordModels);
        }
    }
}
