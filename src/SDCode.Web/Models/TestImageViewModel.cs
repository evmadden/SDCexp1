namespace SDCode.Web.Models
{
    public class TestImageViewModel
    {
        public TestImageViewModel(string participantID, int progress, string imageToDisplay)
        {
            ParticipantID = participantID;
            Progress = progress;
            ImageToDisplay = imageToDisplay;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string ImageToDisplay { get; }
    }
}
