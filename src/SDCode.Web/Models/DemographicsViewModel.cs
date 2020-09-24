namespace SDCode.Web.Models
{
    public class DemographicsViewModel
    {
        public DemographicsViewModel(string participantID, bool testIsAvailable)
        {
            ParticipantID = participantID;
            TestIsAvailable = testIsAvailable;
        }

        public string ParticipantID {get;}
        public bool TestIsAvailable { get; }
    }
}
