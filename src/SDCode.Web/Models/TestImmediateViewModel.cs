using System;

namespace SDCode.Web.Models
{
    public class TestImmediateViewModel
    {
        public TestImmediateViewModel(string participantID, int progress, string imageUrl)
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
