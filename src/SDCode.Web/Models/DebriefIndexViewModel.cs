using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class DebriefIndexViewModel
    {
        public DebriefIndexViewModel(string participantID, IReadOnlyCollection<Config.ContactInfo> researchers, string debriefHtml)
        {
            ParticipantID = participantID;
            Researchers = researchers;
            DebriefHtml = debriefHtml;
        }

        public string ParticipantID {get;}
        public IReadOnlyCollection<Config.ContactInfo> Researchers { get; }
        public string DebriefHtml { get; }
    }
}
