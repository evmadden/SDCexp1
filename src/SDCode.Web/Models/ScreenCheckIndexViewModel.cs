namespace SDCode.Web.Models
{
    public class ScreenCheckIndexViewModel
    {
        public ScreenCheckIndexViewModel(string participantID, string nextActionAfterScreenCheck)
        {
            ParticipantID = participantID;
            NextActionAfterScreenCheck = nextActionAfterScreenCheck;
        }

        public string ParticipantID { get; }
        public string NextActionAfterScreenCheck { get; }
    }
}