namespace SDCode.Web.Models
{
    public class TestWaitViewModel
    {
        public TestWaitViewModel(string participantID, string whenToReturn)
        {
            ParticipantID = participantID;
            WhenToReturn = whenToReturn;
        }

        public string ParticipantID {get;}
        public string WhenToReturn { get; }
    }
}
