using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class TestWelcomeBackViewModel
    {
        public TestWelcomeBackViewModel(string participantID, TestWelcomeBackAction action, int progress, string testName)
        {
            ParticipantID = participantID;
            Action = action;
            Progress = progress;
            TestName = testName;
        }

        public string ParticipantID {get;}
        public TestWelcomeBackAction Action { get; }
        public int Progress { get; }
        public string TestName { get; }
    }
}
