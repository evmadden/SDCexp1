namespace SDCode.Web.Models
{
    public class EncodingFinishedViewModel
    {
        public EncodingFinishedViewModel(string participantID) {
            ParticipantID = participantID;
        }

        public string ParticipantID { get; }
    }
}