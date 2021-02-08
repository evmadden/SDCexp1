using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class DebriefIndexViewModel
    {
        public DebriefIndexViewModel(string participantID, IReadOnlyCollection<Config.ContactInfo> researchers)
        {
            ParticipantID = participantID;
            Researchers = researchers;
        }

        public string ParticipantID {get;}
        public IReadOnlyCollection<Config.ContactInfo> Researchers { get; }
    }
}
