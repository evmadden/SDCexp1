using System;

namespace SDCode.Web.Models.Data
{
    public class SessionMetaDbModel
    {
        public string ParticipantID { get; set; }
        public string SessionName { get; set; }
        public string NeglectedReason { get; set; }
        public string ObscuredReason { get; set; }
        public DateTime? FinishedWhenUtc { get; set; }
    }
}
