namespace SDCode.Web.Models
{
    public class HomeConsentInfoViewModel
    {
        public HomeConsentInfoViewModel(string participantID, bool showLanguageRequirement)
        {
            ParticipantID = participantID;
            ShowLanguageRequirement = showLanguageRequirement;
        }

        public string ParticipantID {get;}
        public bool ShowLanguageRequirement {get;}
    }
}
