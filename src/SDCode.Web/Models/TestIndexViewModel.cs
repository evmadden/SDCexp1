using System;

namespace SDCode.Web.Models
{
    public class TestIndexViewModel
    {
        public TestIndexViewModel(string participantID, int progress, string testName)
        {
            ParticipantID = participantID;
            Progress = progress;
            TestName = testName;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string TestName { get; }
    }
}
