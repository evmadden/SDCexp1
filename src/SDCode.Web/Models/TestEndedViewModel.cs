using System.Collections.Generic;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class TestEndedViewModel
    {
        public TestEndedViewModel(string participantID, string testName, IReadOnlyCollection<Config.ContactInfo> researchers) {
            ParticipantID = participantID;
            TestName = testName;
            Researchers = researchers;
        }

        public string ParticipantID { get; }
        public string TestName { get; }
        public IReadOnlyCollection<Config.ContactInfo> Researchers { get; }
    }
}