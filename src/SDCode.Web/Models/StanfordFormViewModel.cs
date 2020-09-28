namespace SDCode.Web.Models
{
    public class StanfordFormViewModel
    {
        public StanfordFormViewModel(string participantID, string formAction, string nextAction, bool isEncoding)
        {
            ParticipantID = participantID;
            FormAction = formAction;
            NextAction = nextAction;
            IsEncoding = isEncoding;
        }

        public string ParticipantID {get;}
        public string FormAction { get; }
        public string NextAction { get; }
        public bool IsEncoding { get; }
    }
}
