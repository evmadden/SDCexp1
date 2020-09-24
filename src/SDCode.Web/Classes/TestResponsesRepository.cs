using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestResponsesRepository
    {
        IEnumerable<ResponseDataModel> Read(string participantID, string testName);
        void Add(string participantID, string testName, ResponseDataModel model);
        int GetCount(string participantID, string testName);
        void Archive(string participantID, string testName);
    }

    public class TestResponsesRepository : ITestResponsesRepository
    {
        private readonly ICsvFile<ResponseDataModel, ResponseDataModel.Map> _responseDataCsvFile;

        public TestResponsesRepository(ICsvFile<ResponseDataModel, ResponseDataModel.Map> responseDataCsvFile)
        {
            _responseDataCsvFile = responseDataCsvFile;
        }

        public void Add(string participantID, string testName, ResponseDataModel model) {
            var csvFile = GetResponseDataCsvFile(participantID, testName);
            var records = csvFile.Read().ToList();
            records.Add(model);
            csvFile = GetResponseDataCsvFile(participantID, testName);
            csvFile.Write(records);
        }

        public void Archive(string participantID, string testName)
        {
            var csvFile = GetResponseDataCsvFile(participantID, testName);
            var responses = csvFile.Read().ToList();
            if (responses.Any()) {
                var firstResponseWhenUtc = responses.Select(x=>x.WhenUtc).Min().ToString("yyyyMMddHHmmss");
                csvFile = _responseDataCsvFile.WithFilename($"{participantID}_{testName}_{firstResponseWhenUtc}");
                csvFile.Write(responses);
            }
            Clear(participantID, testName);
        }

        private void Clear(string participantID, string testName)
        {
            var csvFile = GetResponseDataCsvFile(participantID, testName);
            csvFile.Write(new List<ResponseDataModel>());
        }

        public int GetCount(string participantID, string testName)
        {
            var records = Read(participantID, testName);
            var result = records.Count();
            return result;
        }

        private ICsvFile<ResponseDataModel, ResponseDataModel.Map> GetResponseDataCsvFile(string participantID, string testName) {
            var csvFilename = $"{participantID}_{testName}";
            var result = _responseDataCsvFile.WithFilename(csvFilename);
            return result;
        }

        public IEnumerable<ResponseDataModel> Read(string participantID, string testName)
        {
            var csvFile = GetResponseDataCsvFile(participantID, testName);
            var result = csvFile.Read();
            return result;
        }
    }
}
