namespace SDCode.Web.Models
{
    public class TestQuestionsViewModel
    {
        public TestQuestionsViewModel(string participantID, string testName)
        {
            ParticipantID = participantID;
            TestName = testName;
        }

        public string ParticipantID {get;}
        public string TestName { get; }
    }
}
