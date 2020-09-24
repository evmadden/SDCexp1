namespace SDCode.Web.Models
{
    public class HomeConsentInfoViewModel
    {
        public HomeConsentInfoViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
