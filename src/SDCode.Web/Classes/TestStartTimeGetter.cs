using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace SDCode.Web.Classes
{
    public interface ITestStartTimeGetter
    {
        DateTime GetUtc(string participantID);
    }

    public class TestStartTimeGetter : ITestStartTimeGetter
    {
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly IProgressGetter _progressGetter;
        private readonly ITestNameGetter _testNameGetter;
        private readonly ITestResponsesRepository _testResponsesRepository;
        private readonly Config _config;
        private readonly ISessionMetaRepository _sessionMetaRepository;

        public TestStartTimeGetter(IPhaseSetsGetter phaseSetsGetter, IProgressGetter progressGetter, ITestNameGetter testNameGetter, ITestResponsesRepository testResponsesRepository, IOptions<Config> config, ISessionMetaRepository sessionMetaRepository)
        {
            _phaseSetsGetter = phaseSetsGetter;
            _progressGetter = progressGetter;
            _testNameGetter = testNameGetter;
            _testResponsesRepository = testResponsesRepository;
            _config = config.Value;
            _sessionMetaRepository = sessionMetaRepository;
        }

        public DateTime GetUtc(string participantID)
        {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(phaseSets, progress);
            var testStartDelays = new Dictionary<string, TimeSpan>() {{nameof(phaseSets.Delayed), new TimeSpan(_config.TestWaitDelayedDays,0,0,0) }, {nameof(phaseSets.Followup), new TimeSpan(_config.TestWaitFollowupDays,0,0,0)}};
            var testStartDelay = testStartDelays.ContainsKey(testName) ? testStartDelays[testName] : default;
            bool testNameIsImmediate = string.Equals(testName, nameof(phaseSets.Immediate));
            DateTime priorPhaseStartTimeUtc;
            if (testNameIsImmediate) {
                var encodingSessionMeta = _sessionMetaRepository.Get(participantID, "Encoding");
                priorPhaseStartTimeUtc = encodingSessionMeta.FinishedWhenUtc.HasValue ? encodingSessionMeta.FinishedWhenUtc.Value : throw new Exception("Encoding's FinishedWhenUtc missing unexpectedly.");
            } else {
                var priorTestName = _testNameGetter.Get(phaseSets, progress-1);
                var priorTestResponses = _testResponsesRepository.Read(participantID, priorTestName);
                priorPhaseStartTimeUtc = priorTestResponses.Select(x=>x.WhenUtc).DefaultIfEmpty().Min();
            }
            var result = priorPhaseStartTimeUtc + testStartDelay;
            return result;
        }
    }
}
