using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestWelcomeViewModel
    {
        public TestWelcomeViewModel(string participantID, TestInstructionsViewModel testInstructionsViewModel) {
            ParticipantID = participantID;
            TestInstructionsViewModel = testInstructionsViewModel;
        }

        public string ParticipantID { get; }
        public TestInstructionsViewModel TestInstructionsViewModel { get; }
    }
}