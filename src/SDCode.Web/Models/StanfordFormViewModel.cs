namespace SDCode.Web.Models
{
    public class StanfordFormViewModel
    {
        public StanfordFormViewModel(string participantID, string nextAction, bool isEncoding)
        {
            ParticipantID = participantID;
            NextAction = nextAction;
            IsEncoding = isEncoding;
        }

        public string ParticipantID {get;}
        public string NextAction { get; }
        public bool IsEncoding { get; }
    }
}
