using System;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.Data
{
    public class StanfordDbModel
    {
        public string ParticipantID { get; set; }
        public Sleepinesses? Immediate { get; set; }
        public DateTime? ImmediateUtc { get; set; }
        public Sleepinesses? Delayed { get; set; }
        public DateTime? DelayedUtc { get; set; }
        public Sleepinesses? Followup { get; set; }
        public DateTime? FollowupUtc { get; set; }
    }
}
