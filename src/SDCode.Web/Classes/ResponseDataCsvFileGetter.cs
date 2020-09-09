using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IResponseDataCsvFileGetter
    {
        ICsvFile<ResponseDataModel, ResponseDataModel.Map> Get(string participantID, string testName);
    }

    public class ResponseDataCsvFileGetter : IResponseDataCsvFileGetter
    {
        private readonly ICsvFile<ResponseDataModel, ResponseDataModel.Map> _responseDataCsvFile;

        public ResponseDataCsvFileGetter(ICsvFile<ResponseDataModel, ResponseDataModel.Map> responseDataCsvFile)
        {
            _responseDataCsvFile = responseDataCsvFile;
        }

        public ICsvFile<ResponseDataModel, ResponseDataModel.Map> Get(string participantID, string testName)
        {
            var csvFilename = $"{participantID}_{testName}";
            var result = _responseDataCsvFile.WithFilename(csvFilename);
            return result;
        }
    }
}
