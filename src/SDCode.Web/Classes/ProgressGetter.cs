using System.Linq;

namespace SDCode.Web.Classes
{
    public interface IProgressGetter
    {
        int Get(string participantID);
    }

    public class ProgressGetter : IProgressGetter
    {
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly ITestResponsesRepository _testResponsesRepository;

        public ProgressGetter(IPhaseSetsGetter phaseSetsGetter, ITestResponsesRepository testResponsesRepository)
        {
            _phaseSetsGetter = phaseSetsGetter;
            _testResponsesRepository = testResponsesRepository;
        }

        public int Get(string participantID)
        {
            int result;
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var immediateResponsesCount = _testResponsesRepository.GetResponsesFromMostRecentSession(participantID, nameof(phaseSets.Immediate)).Count();
            var immediateTestComplete = immediateResponsesCount == phaseSets.Immediate.Count();
            if (immediateTestComplete) {
                var delayedResponsesCount = _testResponsesRepository.GetResponsesFromMostRecentSession(participantID, nameof(phaseSets.Delayed)).Count();
                var delayedTestComplete = delayedResponsesCount == phaseSets.Delayed.Count();
                if (delayedTestComplete) {
                    var followupResponsesCount = _testResponsesRepository.GetResponsesFromMostRecentSession(participantID, nameof(phaseSets.Followup)).Count();
                    var followupTestComplete = followupResponsesCount == phaseSets.Followup.Count();
                    if (followupTestComplete) {
                        result = phaseSets.Immediate.Count() + phaseSets.Delayed.Count() + phaseSets.Followup.Count();
                    } else {
                        result = phaseSets.Immediate.Count() + phaseSets.Delayed.Count();
                    }
                } else {
                    result = phaseSets.Immediate.Count();
                }
            } else {
                result = 0;
            }
            return result;
        }
    }
}
