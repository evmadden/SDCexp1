namespace SDCode.Web.Models
{
    public class TestEndedViewModel
    {
        public TestEndedViewModel(string participantID, string testName, string testWaitDescriptionDelayed, string testWaitDescriptionFollowup) {
            ParticipantID = participantID;
            TestName = testName;
            TestWaitDescriptionDelayed = testWaitDescriptionDelayed;
            TestWaitDescriptionFollowup = testWaitDescriptionFollowup;
        }

        public string ParticipantID { get; }
        public string TestName { get; }
        public string TestWaitDescriptionDelayed { get; }
        public string TestWaitDescriptionFollowup { get; }
    }
}