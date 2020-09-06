using System;

namespace SDCode.Web.Models
{
    public class StanfordViewModel
    {
        public StanfordViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
