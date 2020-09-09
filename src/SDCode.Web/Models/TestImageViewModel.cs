namespace SDCode.Web.Models
{
    public class TestImageViewModel
    {
        public TestImageViewModel(string participantID, int progress, string imageUrl)
        {
            ParticipantID = participantID;
            Progress = progress;
            ImageUrl = imageUrl;
        }

        public string ParticipantID {get;}
        public int Progress { get; }
        public string ImageUrl { get; }
    }
}
