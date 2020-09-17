using System;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class TestWelcomeBackViewModel
    {
        public TestWelcomeBackViewModel(string participantID, TestWelcomeBackAction action, int progress, string testName, System.DateTime? nextTestWhenUtc)
        {
            ParticipantID = participantID;
            Action = action;
            Progress = progress;
            TestName = testName;
            NextTestWhenUtc = nextTestWhenUtc;
        }

        public string ParticipantID {get;}
        public TestWelcomeBackAction Action { get; }
        public int Progress { get; }
        public string TestName { get; }
        public DateTime? NextTestWhenUtc { get; }
    }
}
