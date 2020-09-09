using System.Linq;

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

        public ProgressGetter(IResponseDataCsvFileGetter responseDataCsvFileGetter, ITestSetsGetter testSetsGetter)
        {
            _responseDataCsvFileGetter = responseDataCsvFileGetter;
            _testSetsGetter = testSetsGetter;
        }

        // todo mlh refactor to remove Write() side-effect
        public int Get(string participantID)
        {
            int result;
            var testSets = _testSetsGetter.Get(participantID);
            var immediateResponses = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Immediate)).Read().ToList();
            bool immediateTestComplete = immediateResponses.Count == testSets.Immediate.Count();
            if (immediateTestComplete) {
                var delayedResponses = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Delayed)).Read().ToList();
                bool delayedTestComplete = delayedResponses.Count == testSets.Delayed.Count();
                if (delayedTestComplete) {
                    var followupResponses = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Followup)).Read().ToList();
                    result = testSets.Immediate.Count() + testSets.Delayed.Count() - 1;
                    followupResponses.Clear();
                    _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Followup)).Write(followupResponses);
                } else {
                    result = testSets.Immediate.Count() - 1;
                    delayedResponses.Clear();
                    _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Delayed)).Write(delayedResponses);
                }
            } else {
                result = 0;
                immediateResponses.Clear();
                _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Immediate)).Write(immediateResponses);
            }
            return result;
        }
    }
}
