using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class HomeConsentInfoViewModel
    {
        public HomeConsentInfoViewModel(string participantID, bool showLanguageRequirement, IReadOnlyCollection<Config.ContactInfo> researchers, IReadOnlyCollection<Config.ContactInfo> principleInvestigators)
        {
            ParticipantID = participantID;
            ShowLanguageRequirement = showLanguageRequirement;
            Researchers = researchers;
            PrincipleInvestigators = principleInvestigators;
        }

        public string ParticipantID {get;}
        public bool ShowLanguageRequirement {get;}
        public IReadOnlyCollection<Config.ContactInfo> Researchers { get; }
        public IReadOnlyCollection<Config.ContactInfo> PrincipleInvestigators { get; }
    }
}
