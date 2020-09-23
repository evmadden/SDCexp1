using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class ImageCheckIndexViewModel
    {
        public ImageCheckIndexViewModel(string participantID, Sleepinesses? stanford, string nextActionAfterImageCheck)
        {
            ParticipantID = participantID;
            Stanford = stanford;
            NextActionAfterImageCheck = nextActionAfterImageCheck;
        }

        public string ParticipantID {get;}
        public Sleepinesses? Stanford { get; }
        public string NextActionAfterImageCheck { get; }
    }
}
