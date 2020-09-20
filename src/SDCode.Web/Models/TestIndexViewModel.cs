using System;

namespace SDCode.Web.Models
{
    public class TestIndexViewModel
    {
        public TestIndexViewModel(string participantID, int progress, string testName, int feedbackDisplayDurationInMilliseconds)
        {
            ParticipantID = participantID;
            Progress = progress;
            TestName = testName;
            FeedbackDisplayDurationInMilliseconds = feedbackDisplayDurationInMilliseconds;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string TestName { get; }
        public int FeedbackDisplayDurationInMilliseconds { get; }
    }
}
