namespace SDCode.Web.Models
{
    public class HomeWelcomeBackViewModel
    {
        public HomeWelcomeBackViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
