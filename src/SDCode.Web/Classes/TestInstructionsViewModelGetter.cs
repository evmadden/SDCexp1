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

        public TestInstructionsViewModelGetter(IJudgementsDescriptionGetter judgementsDescriptionGetter, IConfidencesDescriptionsGetter confidencesDescriptionsGetter, IPhaseSetsGetter phaseSetsGetter)
        {
            _judgementsDescriptionGetter = judgementsDescriptionGetter;
            _confidencesDescriptionsGetter = confidencesDescriptionsGetter;
            _phaseSetsGetter = phaseSetsGetter;
        }

        public TestInstructionsViewModel Get(string participantID)
        {
            var oldJudgementsDescription = _judgementsDescriptionGetter.Get(Judgements.Old);
            var newJudgementsDescription = _judgementsDescriptionGetter.Get(Judgements.New);
            var confidenceDescriptions = _confidencesDescriptionsGetter.Get().ToDictionary(x=>$"{x.Key}", x=>x.Value);
            var stimuliCount = _phaseSetsGetter.Get(participantID).EncodingCount;
            var result = new TestInstructionsViewModel(oldJudgementsDescription, newJudgementsDescription, confidenceDescriptions, stimuliCount);
            return result;
        }
    }
}
