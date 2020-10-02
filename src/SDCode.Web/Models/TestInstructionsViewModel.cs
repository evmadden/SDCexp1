using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestInstructionsViewModel
    {
        public TestInstructionsViewModel(string oldJudgementsDescription, string newJudgementsDescription, IDictionary<string, string> confidenceDescriptions)
        {
            OldJudgementsDescription = oldJudgementsDescription;
            NewJudgementsDescription = newJudgementsDescription;
            ConfidenceDescriptions = confidenceDescriptions;
        }

        public string OldJudgementsDescription { get; }
        public string NewJudgementsDescription { get; }
        public IDictionary<string, string> ConfidenceDescriptions { get; } // <int, string> not supported: https://github.com/dotnet/runtime/issues/30524#issuecomment-519333400
    }
}
