namespace SDCode.Web.Models
{
    public class TestWelcomeViewModel
    {
        public TestWelcomeViewModel(string participantID) {
            ParticipantID = participantID;
        }

        public string ParticipantID { get; }
    }
}