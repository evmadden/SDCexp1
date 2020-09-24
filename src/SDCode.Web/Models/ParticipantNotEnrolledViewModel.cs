namespace SDCode.Web.Models
{
    public class ParticipantNotEnrolledViewModel
    {
        public ParticipantNotEnrolledViewModel(string participantID) {
            ParticipantID = participantID;
        }

        public string ParticipantID { get; }
    }
}