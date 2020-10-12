using System;
using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestIndexViewModel
    {
        public TestIndexViewModel(string participantID, int progress, string testName, Guid sessionID, int feedbackDisplayDurationInMilliseconds, bool shouldAutomate, int automationDelayInMilliseconds, IEnumerable<string> imageTypesToPreload, TestInstructionsViewModel testInstructionsViewModel, string imageTypesUrlTemplate)
        {
            ParticipantID = participantID;
            Progress = progress;
            TestName = testName;
            SessionID = sessionID;
            FeedbackDisplayDurationInMilliseconds = feedbackDisplayDurationInMilliseconds;
            ShouldAutomate = shouldAutomate;
            AutomationDelayInMilliseconds = automationDelayInMilliseconds;
            ImageTypesToPreload = imageTypesToPreload;
            TestInstructionsViewModel = testInstructionsViewModel;
            ImageTypesUrlTemplate = imageTypesUrlTemplate;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string TestName { get; }
        public Guid SessionID { get; }
        public int FeedbackDisplayDurationInMilliseconds { get; }
        public bool ShouldAutomate { get; }
        public int AutomationDelayInMilliseconds { get; }
        public IEnumerable<string> ImageTypesToPreload { get; }
        public TestInstructionsViewModel TestInstructionsViewModel { get; }
        public string ImageTypesUrlTemplate { get; }
    }
}
