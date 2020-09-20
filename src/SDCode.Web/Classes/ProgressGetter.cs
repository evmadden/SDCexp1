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

        public ProgressGetter(IResponseDataCsvFileGetter responseDataCsvFileGetter, ITestSetsGetter testSetsGetter)
        {
            _responseDataCsvFileGetter = responseDataCsvFileGetter;
            _testSetsGetter = testSetsGetter;
        }

        // todo mlh refactor to remove Write() side-effects
        public int Get(string participantID)
        {
            int result;
            var testSets = _testSetsGetter.Get(participantID);
            var immediateCsvFile = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Immediate));
            var immediateResponses = immediateCsvFile.Read().ToList();
            bool immediateTestComplete = immediateResponses.Count == testSets.Immediate.Count();
            if (immediateTestComplete) {
                var delayedCsvFile = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Delayed));
                var delayedResponses = delayedCsvFile.Read().ToList();
                bool delayedTestComplete = delayedResponses.Count == testSets.Delayed.Count();
                if (delayedTestComplete) {
                    var followupCsvFile = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Followup));
                    var followupResponses = followupCsvFile.Read().ToList();
                    bool followupTestComplete = followupResponses.Count == testSets.Followup.Count();
                    if (followupTestComplete) {
                        result = testSets.Immediate.Count() + testSets.Delayed.Count() + testSets.Followup.Count();
                    } else {
                        KeepOldResponses(participantID, nameof(testSets.Followup), followupCsvFile, followupResponses);
                        result = testSets.Immediate.Count() + testSets.Delayed.Count();
                        followupResponses.Clear();
                        followupCsvFile = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Followup));
                        followupCsvFile.Write(followupResponses);
                    }
                } else {
                    KeepOldResponses(participantID, nameof(testSets.Delayed), delayedCsvFile, delayedResponses);
                    result = testSets.Immediate.Count();
                    delayedResponses.Clear();
                    delayedCsvFile = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Delayed));
                    delayedCsvFile.Write(delayedResponses);
                }
            } else {
                KeepOldResponses(participantID, nameof(testSets.Immediate), immediateCsvFile, immediateResponses);
                result = 0;
                immediateResponses.Clear();
                immediateCsvFile = _responseDataCsvFileGetter.Get(participantID, nameof(testSets.Immediate));
                immediateCsvFile.Write(immediateResponses);
            }
            return result;
        }

        private void KeepOldResponses<T, TMap>(string participantID, string testName, ICsvFile<T, TMap> csvFile, IEnumerable<T> responses) where T : ResponseDataModel
        {
            if (responses.Any()) {
                csvFile.WithFilename($"{participantID}_{testName}_{responses.Select(x=>x.WhenUtc).Min().ToString("yyyyMMddHHmmss")}").Write(responses);
            }
        }
    }
}
