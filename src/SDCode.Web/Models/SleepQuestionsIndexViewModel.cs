namespace SDCode.Web.Models
{
    public class SleepQuestionsIndexViewModel
    {
        public SleepQuestionsIndexViewModel(string participantID)
        {
            ParticipantID = participantID;
        }

        public string ParticipantID { get; }
    }
}