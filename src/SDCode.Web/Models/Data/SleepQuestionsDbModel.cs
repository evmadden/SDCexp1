namespace SDCode.Web.Models.Data
{
    public class SleepQuestionsDbModel
    {
        public string ParticipantID { get; set; }
        public string ImmediateBed{ get; set; }
        public string ImmediateWake{ get; set; }
        public string ImmediateLatency{ get; set; }
        public string ImmediateTST{ get; set; }
        public string DelayedBed{ get; set; }
        public string DelayedWake{ get; set; }
        public string DelayedLatency{ get; set; }
        public string DelayedTST{ get; set; }
        public string FollowupBed{ get; set; }
        public string FollowupWake{ get; set; }
        public string FollowupLatency{ get; set; }
        public string FollowupTST{ get; set; }
    }
}
