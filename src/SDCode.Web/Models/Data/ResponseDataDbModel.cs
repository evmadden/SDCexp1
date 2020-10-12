using System;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.Data
{
    public class ResponseDbDataModel
    {
        public string ParticipantID { get; set; }
        public string TestName { get; set; }
        public Guid SessionID { get; set; }
        public string Image { get; set; }
        public Congruencies Congruency { get; set; }
        public Contexts Context { get; set; }
        public Judgements Judgement { get; set; }
        public Confidences Confidence { get; set; }
        public long ReactionTime { get; set; }
        public Feedbacks Feedback { get; set; }
        public DateTime WhenUtc { get; set; }
    }
}
