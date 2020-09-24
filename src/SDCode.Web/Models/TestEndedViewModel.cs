namespace SDCode.Web.Models
{
    public class TestEndedViewModel
    {
        public TestEndedViewModel(string participantID, string testName) {
            ParticipantID = participantID;
            TestName = testName;
        }

        public string ParticipantID { get; }
        public string TestName { get; }
    }
}