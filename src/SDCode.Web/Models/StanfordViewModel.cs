namespace SDCode.Web.Models
{
    public class StanfordViewModel
    {
        public StanfordViewModel(string participantID, bool isEncoding)
        {
            ParticipantID = participantID;
            IsEncoding = isEncoding;
        }

        public string ParticipantID {get;}
        public bool IsEncoding { get; }
    }
}
