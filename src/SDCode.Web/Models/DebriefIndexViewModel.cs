namespace SDCode.Web.Models
{
    public class DebriefIndexViewModel
    {
        public DebriefIndexViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
