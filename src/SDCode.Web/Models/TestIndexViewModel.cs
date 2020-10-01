using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestIndexViewModel
    {
        public TestIndexViewModel(string participantID, int progress, string testName, int feedbackDisplayDurationInMilliseconds, bool shouldAutomate, int automationDelayInMilliseconds, IEnumerable<string> imageTypesToPreload)
        {
            ParticipantID = participantID;
            Progress = progress;
            TestName = testName;
            FeedbackDisplayDurationInMilliseconds = feedbackDisplayDurationInMilliseconds;
            ShouldAutomate = shouldAutomate;
            AutomationDelayInMilliseconds = automationDelayInMilliseconds;
            ImageTypesToPreload = imageTypesToPreload;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string TestName { get; }
        public int FeedbackDisplayDurationInMilliseconds { get; }
        public bool ShouldAutomate { get; }
        public int AutomationDelayInMilliseconds { get; }
        public IEnumerable<string> ImageTypesToPreload { get; }
    }
}
