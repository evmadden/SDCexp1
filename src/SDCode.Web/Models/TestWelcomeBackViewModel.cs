using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestWelcomeBackViewModel
    {
        public TestWelcomeBackViewModel(string participantID, TestInstructionsViewModel testInstructionsViewModel)
        {
            ParticipantID = participantID;
            TestInstructionsViewModel = testInstructionsViewModel;
        }

        public string ParticipantID {get;}
        public TestInstructionsViewModel TestInstructionsViewModel { get; }
    }
}
