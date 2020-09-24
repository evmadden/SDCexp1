namespace SDCode.Web.Models
{
    public class StanfordFormViewModel
    {
        public StanfordFormViewModel(string participantID, string nextAction)
        {
            ParticipantID = participantID;
            NextAction = nextAction;
        }

        public string ParticipantID {get;}
        public string NextAction { get; }
    }
}
