using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SDCode.Web.Models
{
    public class SleepQuestionsModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(ImmediateBed))]
        [Description("Participant notes before Immediate what time they went to bed the previous night.")] 
        public string ImmediateBed{ get; set; }
        [Name(nameof(ImmediateWake))]
        [Description("Participant notes before Immediate what time they woke the day of the study.")]
        public string ImmediateWake{ get; set; }
        [Name(nameof(ImmediateLatency))]
        [Description("Participant notes before Immediate how long it took them to fall asleep the previous night.")]
        public string ImmediateLatency{ get; set; }
        [Name(nameof(ImmediateTST))]
        [Description("Participant notes before Immediate their total sleep time for the previous night.")]
        public string ImmediateTST{ get; set; }
        [Description("Participant notes before Immediate what time they went to bed the previous night.")] 
        public string DelayedBed{ get; set; }
        [Name(nameof(DelayedWake))]
        [Description("Participant notes before Delayed what time they woke the day of the study.")]
        public string DelayedWake{ get; set; }
        [Name(nameof(DelayedLatency))]
        [Description("Participant notes before Delayed how long it took them to fall asleep the previous night.")]
        public string DelayedLatency{ get; set; }
        [Name(nameof(DelayedTST))]
        [Description("Participant notes before Delayed their total sleep time for the previous night.")]
        public string DelayedTST{ get; set; }
        [Description("Participant notes before Followup what time they went to bed the previous night.")] 
        public string FollowupBed{ get; set; }
        [Name(nameof(FollowupWake))]
        [Description("Participant notes before Followup what time they woke the day of the study.")]
        public string FollowupWake{ get; set; }
        [Name(nameof(FollowupLatency))]
        [Description("Participant notes before Followup how long it took them to fall asleep the previous night.")]
        public string FollowupLatency{ get; set; }
        [Name(nameof(FollowupTST))]
        [Description("Participant notes before Followup their total sleep time for the previous night.")]
        public string FollowupTST{ get; set; }

        public sealed class Map : ClassMap<SleepQuestionsModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.ImmediateBed).Name(nameof(ImmediateBed));
                Map(m => m.ImmediateWake).Name(nameof(ImmediateWake));
                Map(m => m.ImmediateLatency).Name(nameof(ImmediateLatency));
                Map(m => m.ImmediateTST).Name(nameof(ImmediateTST));
                Map(m => m.DelayedBed).Name(nameof(DelayedBed));
                Map(m => m.DelayedWake).Name(nameof(DelayedWake));
                Map(m => m.DelayedLatency).Name(nameof(DelayedLatency));
                Map(m => m.DelayedTST).Name(nameof(DelayedTST));
                Map(m => m.FollowupBed).Name(nameof(FollowupBed));
                Map(m => m.FollowupWake).Name(nameof(FollowupWake));
                Map(m => m.FollowupLatency).Name(nameof(FollowupLatency));
                Map(m => m.FollowupTST).Name(nameof(FollowupTST));
            }
        }
    }
}
