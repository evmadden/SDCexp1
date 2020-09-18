using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class EncodingImageOutOfBoundsViewModel
    {
        public EncodingImageOutOfBoundsViewModel(string participantID, Sleepinesses stanford)
        {
            ParticipantID = participantID;
            Stanford = stanford;
        }

        public string ParticipantID {get;}
        public Sleepinesses Stanford { get; }
    }
}
