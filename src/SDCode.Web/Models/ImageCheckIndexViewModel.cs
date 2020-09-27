using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class ImageCheckIndexViewModel
    {
        public ImageCheckIndexViewModel(string participantID, Sleepinesses? stanford, string nextActionAfterImageCheck, bool showSpacebarOrientation)
        {
            ParticipantID = participantID;
            Stanford = stanford;
            NextActionAfterImageCheck = nextActionAfterImageCheck;
            ShowSpacebarOrientation = showSpacebarOrientation;
        }

        public string ParticipantID {get;}
        public Sleepinesses? Stanford { get; }
        public string NextActionAfterImageCheck { get; }
        public bool ShowSpacebarOrientation { get; }
    }
}
