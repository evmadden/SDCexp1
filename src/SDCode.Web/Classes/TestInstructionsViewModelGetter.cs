using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestInstructionsViewModelGetter
    {
        TestInstructionsViewModel Get();
    }

    public class TestInstructionsViewModelGetter : ITestInstructionsViewModelGetter
    {
        private readonly IJudgementsDescriptionGetter _judgementsDescriptionGetter;
        private readonly IConfidencesDescriptionsGetter _confidencesDescriptionsGetter;

        public TestInstructionsViewModelGetter(IJudgementsDescriptionGetter judgementsDescriptionGetter, IConfidencesDescriptionsGetter confidencesDescriptionsGetter)
        {
            _judgementsDescriptionGetter = judgementsDescriptionGetter;
            _confidencesDescriptionsGetter = confidencesDescriptionsGetter;
        }

        public TestInstructionsViewModel Get()
        {
            var oldJudgementsDescription = _judgementsDescriptionGetter.Get(Judgements.Old);
            var newJudgementsDescription = _judgementsDescriptionGetter.Get(Judgements.New);
            var confidenceDescriptions = _confidencesDescriptionsGetter.Get().ToDictionary(x=>$"{x.Key}", x=>x.Value);
            var result = new TestInstructionsViewModel(oldJudgementsDescription, newJudgementsDescription, confidenceDescriptions);
            return result;
        }
    }
}
