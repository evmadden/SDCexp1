namespace SDCode.Web.Models
{
    public class EncodingInstructionsViewModel
    {
        public EncodingInstructionsViewModel(string participantID, string nextActionAfterImageCheck, bool showSpacebarOrientation)
        {
            ParticipantID = participantID;
            NextActionAfterImageCheck = nextActionAfterImageCheck;
            ShowSpacebarOrientation = showSpacebarOrientation;
        }
        public string ParticipantID {get;}
        public string NextActionAfterImageCheck { get; }
        public bool ShowSpacebarOrientation { get; }
    }
}
