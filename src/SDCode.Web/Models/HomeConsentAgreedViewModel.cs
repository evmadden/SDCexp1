namespace SDCode.Web.Models
{
    public class HomeConsentAgreedViewModel
    {
        public HomeConsentAgreedViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
