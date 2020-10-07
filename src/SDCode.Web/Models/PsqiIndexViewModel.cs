namespace SDCode.Web.Models
{
    public class PsqiIndexViewModel
    {
        public PsqiIndexViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
