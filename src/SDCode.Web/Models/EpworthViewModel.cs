namespace SDCode.Web.Models
{
    public class EpworthViewModel
    {
        public EpworthViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID {get;}
    }
}
