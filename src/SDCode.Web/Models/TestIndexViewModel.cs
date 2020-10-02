using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestIndexViewModel
    {
        public TestIndexViewModel(string participantID, int progress, string testName, int feedbackDisplayDurationInMilliseconds, bool shouldAutomate, int automationDelayInMilliseconds, IEnumerable<string> imageTypesToPreload, IDictionary<string, string> confidenceDescriptions, string oldDescription, string newDescription)
        {
            ParticipantID = participantID;
            Progress = progress;
            TestName = testName;
            FeedbackDisplayDurationInMilliseconds = feedbackDisplayDurationInMilliseconds;
            ShouldAutomate = shouldAutomate;
            AutomationDelayInMilliseconds = automationDelayInMilliseconds;
            ImageTypesToPreload = imageTypesToPreload;
            ConfidenceDescriptions = confidenceDescriptions;
            OldDescription = oldDescription;
            NewDescription = newDescription;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string TestName { get; }
        public int FeedbackDisplayDurationInMilliseconds { get; }
        public bool ShouldAutomate { get; }
        public int AutomationDelayInMilliseconds { get; }
        public IEnumerable<string> ImageTypesToPreload { get; }
        public IDictionary<string, string> ConfidenceDescriptions { get; } // https://github.com/dotnet/runtime/issues/30524#issuecomment-519333400
        public string OldDescription { get; }
        public string NewDescription { get; }
    }
}
