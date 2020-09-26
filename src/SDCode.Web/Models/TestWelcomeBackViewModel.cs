using System;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class TestWelcomeBackViewModel
    {
        public TestWelcomeBackViewModel(string participantID, ReturningUserAction action, int progress, string testName, DateTime? nextTestWhenUtc)
        {
            ParticipantID = participantID;
            Action = action;
            Progress = progress;
            TestName = testName;
            NextTestWhenUtc = nextTestWhenUtc;
        }

        public string ParticipantID {get;}
        public ReturningUserAction Action { get; }
        public int Progress { get; }
        public string TestName { get; }
        public DateTime? NextTestWhenUtc { get; }
    }
}
