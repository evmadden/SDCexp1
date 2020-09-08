using System;

namespace SDCode.Web.Models
{
    public class EncodingInstructionsViewModel
    {
        public EncodingInstructionsViewModel(string participantID)
        {
            ParticipantID = participantID;
        }
        public string ParticipantID {get;}
    }
}
