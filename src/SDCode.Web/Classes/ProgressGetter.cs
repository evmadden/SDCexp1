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
            var immediateResponsesCount = _testResponsesRepository.GetCount(participantID, nameof(phaseSets.Immediate));
            var immediateTestComplete = immediateResponsesCount == phaseSets.Immediate.Count();
            if (immediateTestComplete) {
                var delayedResponsesCount = _testResponsesRepository.GetCount(participantID, nameof(phaseSets.Delayed));
                var delayedTestComplete = delayedResponsesCount == phaseSets.Delayed.Count();
                if (delayedTestComplete) {
                    var followupResponsesCount = _testResponsesRepository.GetCount(participantID, nameof(phaseSets.Followup));
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
