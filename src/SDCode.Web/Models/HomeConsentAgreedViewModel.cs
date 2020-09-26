namespace SDCode.Web.Models
{
    public class HomeConsentFormViewModel
    {
        public HomeConsentFormViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
