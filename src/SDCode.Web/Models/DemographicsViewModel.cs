using System;

namespace SDCode.Web.Models
{
    public class DemographicsViewModel
    {
        public DemographicsViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
