using System.Collections.Generic;

namespace SDCode.Web.Classes
{
    public class PhaseSets
    {
        public PhaseSets(string participantID, IEnumerable<string> encoding, IEnumerable<string> immediate, IEnumerable<string> delayed, IEnumerable<string> followup)
        {
            ParticipantID = participantID;
            Encoding = encoding;
            Immediate = immediate;
            Delayed = delayed;
            Followup = followup;
        }

        public string ParticipantID { get; private set; }
        public IEnumerable<string> Encoding { get; private set; }
        public IEnumerable<string> Immediate { get; private set; }
        public IEnumerable<string> Delayed { get; private set; }
        public IEnumerable<string> Followup { get; private set; }
    }
}
