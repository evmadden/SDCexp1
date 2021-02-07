namespace SDCode.Web.Models
{
    public class HomeConsentFormViewModel
    {
        public HomeConsentFormViewModel(string participantID, bool showLanguageRequirement)
        {
            ParticipantID = participantID;
            ShowLanguageRequirement = showLanguageRequirement;
        }

        public string ParticipantID {get;}
        public bool ShowLanguageRequirement {get;}
    }
}
