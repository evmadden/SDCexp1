namespace SDCode.Web.Models
{
    public class DemographicsViewModel
    {
        public DemographicsViewModel(string participantID, bool showLanguageQuestions)
        {
            ParticipantID = participantID;
            ShowLanguageQuestions = showLanguageQuestions;
        }

        public string ParticipantID {get;}
        public bool ShowLanguageQuestions { get; }
    }
}
