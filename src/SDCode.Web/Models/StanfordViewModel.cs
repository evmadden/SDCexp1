using System;

namespace SDCode.Web.Models
{
    public class StanfordViewModel
    {
        public StanfordViewModel(string participantID, string postUrl)
        {
            ParticipantID = participantID;
            PostUrl = postUrl;
        }

        public string ParticipantID {get;}
        public string PostUrl {get;}
    }
}
