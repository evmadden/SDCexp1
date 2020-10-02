using System.Collections.Generic;

namespace SDCode.Web.Models
{
    public class TestWelcomeBackViewModel
    {
        public TestWelcomeBackViewModel(string participantID, bool stanfordNeeded, TestInstructionsViewModel testInstructionsViewModel)
        {
            ParticipantID = participantID;
            StanfordNeeded = stanfordNeeded;
            TestInstructionsViewModel = testInstructionsViewModel;
        }

        public string ParticipantID {get;}
        public bool StanfordNeeded { get; }
        public TestInstructionsViewModel TestInstructionsViewModel { get; }
    }
}
