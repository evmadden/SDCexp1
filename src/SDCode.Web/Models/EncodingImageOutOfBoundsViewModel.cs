namespace SDCode.Web.Models
{
    public class EncodingImageOutOfBoundsViewModel
    {
        public EncodingImageOutOfBoundsViewModel(string participantID, string stanford)
        {
            ParticipantID = participantID;
            Stanford = stanford;
        }

        public string ParticipantID {get;}
        public string Stanford { get; }
    }
}
