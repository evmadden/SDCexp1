namespace SDCode.Web.Models
{
    public class TestWelcomeBackViewModel
    {
        public TestWelcomeBackViewModel(string participantID, bool stanfordNeeded)
        {
            ParticipantID = participantID;
            StanfordNeeded = stanfordNeeded;
        }

        public string ParticipantID {get;}
        public bool StanfordNeeded { get; }
    }
}
