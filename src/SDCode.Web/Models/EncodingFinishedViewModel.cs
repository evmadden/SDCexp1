using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class EncodingFinishedViewModel
    {
        public EncodingFinishedViewModel(string participantID, System.Collections.Generic.IEnumerable<int> neglectedIndexes) {
            ParticipantID = participantID;
            NeglectedIndexes = neglectedIndexes;
        }

        public string ParticipantID { get; }
        public IEnumerable<int> NeglectedIndexes { get; }
    }
}