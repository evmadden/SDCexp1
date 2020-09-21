using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface IProgressGetter
    {
        int Get(string participantID);
    }

    public class ProgressGetter : IProgressGetter
    {
        private readonly IResponseDataCsvFileGetter _responseDataCsvFileGetter;
        private readonly ITestSetsGetter _testSetsGetter;
        private readonly ITestResponsesRepository _testResponsesRepository;

        public ProgressGetter(IResponseDataCsvFileGetter responseDataCsvFileGetter, ITestSetsGetter testSetsGetter, ITestResponsesRepository testResponsesRepository)
        {
            _responseDataCsvFileGetter = responseDataCsvFileGetter;
            _testSetsGetter = testSetsGetter;
            _testResponsesRepository = testResponsesRepository;
        }

        public int Get(string participantID)
        {
            int result;
            var testSets = _testSetsGetter.Get(participantID);
            var immediateResponsesCount = _testResponsesRepository.GetCount(participantID, nameof(testSets.Immediate));
            bool immediateTestComplete = immediateResponsesCount == testSets.Immediate.Count();
            if (immediateTestComplete) {
                var delayedResponsesCount = _testResponsesRepository.GetCount(participantID, nameof(testSets.Delayed));
                bool delayedTestComplete = delayedResponsesCount == testSets.Delayed.Count();
                if (delayedTestComplete) {
                    var followupResponsesCount = _testResponsesRepository.GetCount(participantID, nameof(testSets.Followup));
                    bool followupTestComplete = followupResponsesCount == testSets.Followup.Count();
                    if (followupTestComplete) {
                        result = testSets.Immediate.Count() + testSets.Delayed.Count() + testSets.Followup.Count();
                    } else {
                        result = testSets.Immediate.Count() + testSets.Delayed.Count();
                    }
                } else {
                    result = testSets.Immediate.Count();
                }
            } else {
                result = 0;
            }
            return result;
        }
    }
}
