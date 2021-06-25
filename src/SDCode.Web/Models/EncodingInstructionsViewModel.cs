using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class EncodingInstructionsViewModel
    {
        public EncodingInstructionsViewModel(string participantID, string nextActionAfterImageCheck, bool showSpacebarOrientation, Sleepinesses? stanford, int stimuliCount, int targetCount)
        {
            ParticipantID = participantID;
            NextActionAfterImageCheck = nextActionAfterImageCheck;
            ShowSpacebarOrientation = showSpacebarOrientation;
            Stanford = stanford;
            StimuliCount = stimuliCount;
            TargetCount = targetCount;
        }
        public string ParticipantID {get;}
        public string NextActionAfterImageCheck { get; }
        public bool ShowSpacebarOrientation { get; }
        public Sleepinesses? Stanford { get; }

        public int StimuliCount {get;}
        public int TargetCount {get;}
    }
}
