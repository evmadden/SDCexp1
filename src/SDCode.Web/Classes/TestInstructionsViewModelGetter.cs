using System;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestInstructionsViewModelGetter
    {
        TestInstructionsViewModel Get(string participantID);
    }

    public class TestInstructionsViewModelGetter : ITestInstructionsViewModelGetter
    {
        private readonly IJudgementsDescriptionGetter _judgementsDescriptionGetter;
        private readonly IConfidencesDescriptionsGetter _confidencesDescriptionsGetter;
        private readonly IPhaseSetsGetter _phaseSetsGetter;
        private readonly IProgressGetter _progressGetter;
        private readonly ITestNameGetter _testNameGetter;

        public TestInstructionsViewModelGetter(IJudgementsDescriptionGetter judgementsDescriptionGetter, IConfidencesDescriptionsGetter confidencesDescriptionsGetter, IPhaseSetsGetter phaseSetsGetter, IProgressGetter progressGetter, ITestNameGetter testNameGetter)
        {
            _judgementsDescriptionGetter = judgementsDescriptionGetter;
            _confidencesDescriptionsGetter = confidencesDescriptionsGetter;
            _phaseSetsGetter = phaseSetsGetter;
            _progressGetter = progressGetter;
            _testNameGetter = testNameGetter;
        }

        public TestInstructionsViewModel Get(string participantID)
        {
            var phaseSets = _phaseSetsGetter.Get(participantID);
            var progress = _progressGetter.Get(participantID);
            var testName = _testNameGetter.Get(phaseSets, progress);
            var oldJudgementsDescription = _judgementsDescriptionGetter.Get(Judgements.Old);
            var newJudgementsDescription = _judgementsDescriptionGetter.Get(Judgements.New);
            var confidenceDescriptions = _confidencesDescriptionsGetter.Get().ToDictionary(x=>$"{x.Key}", x=>x.Value);
            var stimuliCount = Convert.ToInt32(phaseSets.GetType().GetProperty($"{testName}Count").GetValue(phaseSets, null));
            var result = new TestInstructionsViewModel(oldJudgementsDescription, newJudgementsDescription, confidenceDescriptions, stimuliCount);
            return result;
        }
    }
}
