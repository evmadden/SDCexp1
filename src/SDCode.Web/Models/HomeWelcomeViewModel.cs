namespace SDCode.Web.Models
{
    public class HomeWelcomeViewModel
    {
        public HomeWelcomeViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
