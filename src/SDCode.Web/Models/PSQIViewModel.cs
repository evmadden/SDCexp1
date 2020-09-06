using System;

namespace SDCode.Web.Models
{
    public class PSQIViewModel
    {
        public PSQIViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
