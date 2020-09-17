namespace SDCode.Web.Models
{
    public class ImageCheckIndexViewModel
    {
        public ImageCheckIndexViewModel(string participantID, string stanford, string nextActionAfterImageCheck)
        {
            ParticipantID = participantID;
            Stanford = stanford;
            NextActionAfterImageCheck = nextActionAfterImageCheck;
        }

        public string ParticipantID {get;}
        public string Stanford { get; }
        public string NextActionAfterImageCheck { get; }
    }
}
