using System;

namespace SDCode.Web.Models
{
    public class EncodingQuestionsViewModel
    {
        public EncodingQuestionsViewModel(string participantID, bool shouldQuestionNeglected, bool shouldQuestionObscured)
        {
            ParticipantID = participantID;
            ShouldQuestionNeglected = shouldQuestionNeglected;
            ShouldQuestionObscured = shouldQuestionObscured;
        }

        public string ParticipantID {get;}
        public bool ShouldQuestionNeglected { get; }
        public bool ShouldQuestionObscured { get; }
    }
}
