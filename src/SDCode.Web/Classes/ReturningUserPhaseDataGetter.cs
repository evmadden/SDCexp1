using System;
using Microsoft.Extensions.Options;

namespace SDCode.Web.Classes
{
    public interface IReturningUserPhaseDataGetter
    {
        IReturningUserPhaseData Get(string participantID);
    }

    public class ReturningUserPhaseDataGetter : IReturningUserPhaseDataGetter
    {
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly ITestNameGetter _testNameGetter;
        private readonly IProgressGetter _progressGetter;
        private readonly IStanfordRepository _stanfordRepository;
        private readonly ITestStartTimeGetter _testStartTimeGetter;
        private readonly Config _config;
        private IEncodingFinishedChecker _encodingFinishedChecker;

        public ReturningUserPhaseDataGetter(IPhaseSetsGetter phaseSetsGetter, ITestNameGetter testNameGetter, IProgressGetter progressGetter, IStanfordRepository stanfordRepository, ITestStartTimeGetter testStartTimeGetter, IOptions<Config> config, IEncodingFinishedChecker encodingFinishedChecker)
        {
            _phaseSetsGetter = phaseSetsGetter;
            _testNameGetter = testNameGetter;
            _progressGetter = progressGetter;
            _stanfordRepository = stanfordRepository;
            _testStartTimeGetter = testStartTimeGetter;
            _config = config.Value;
            _encodingFinishedChecker = encodingFinishedChecker;
        }

        public IReturningUserPhaseData Get(string participantID)
        {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(phaseSets, progress);
            var stanford = _stanfordRepository.Get(participantID);
            DateTime? nextTestWhenUtc = default;
            ReturningUserAction action;
            var testNameIsImmediate = string.Equals(testName, nameof(phaseSets.Immediate));
            var encodingRequired = !_encodingFinishedChecker.IsFinished(participantID); 
            if (encodingRequired) {
                action = ReturningUserAction.NewUser;
            } else if (testName == default) {
                action = ReturningUserAction.Done;
            } else {
                var nextTestStartTimeBeginUtc = _testStartTimeGetter.GetUtc(participantID);
                var nextTestStartTimeEndUtc = nextTestStartTimeBeginUtc.AddDays(_config.TestTooLateDays);
                DateTime utcNow = DateTime.UtcNow;
                bool mustWait = nextTestStartTimeBeginUtc > utcNow;
                bool tooLate = nextTestStartTimeEndUtc < utcNow;
                if (mustWait) {
                    action = ReturningUserAction.Wait;
                    nextTestWhenUtc = nextTestStartTimeBeginUtc;
                } else if (tooLate) {
                    action = ReturningUserAction.TooLate;
                } else {
                    if (testNameIsImmediate) {
                        action = stanford.LacksImmediate ? ReturningUserAction.Stanford : ReturningUserAction.Test;
                    } else {
                        action = ReturningUserAction.Test;
                    }
                }
            }
            var result = new ReturningUserPhaseData(action, progress, testName, nextTestWhenUtc);
            return result;
        }
    }
}
