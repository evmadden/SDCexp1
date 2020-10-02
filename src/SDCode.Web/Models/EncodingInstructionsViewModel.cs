using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class EncodingInstructionsViewModel
    {
        public EncodingInstructionsViewModel(string participantID, string nextActionAfterImageCheck, bool showSpacebarOrientation, Sleepinesses? stanford)
        {
            ParticipantID = participantID;
            NextActionAfterImageCheck = nextActionAfterImageCheck;
            ShowSpacebarOrientation = showSpacebarOrientation;
            Stanford = stanford;
        }
        public string ParticipantID {get;}
        public string NextActionAfterImageCheck { get; }
        public bool ShowSpacebarOrientation { get; }
        public Sleepinesses? Stanford { get; }
    }
}
