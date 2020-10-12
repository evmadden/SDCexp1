using System;
using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models.CSV
{
    public class StanfordCsvModel
    {
        [Name(nameof(ParticipantID))]
        [Description("ID of the participant.")]
        public string ParticipantID { get; set; }

        [Name(nameof (Immediate))] 
        public Sleepinesses? Immediate { get; set; }
        [Name(nameof (ImmediateUtc))] 
        public DateTime? ImmediateUtc { get; set; }
        [Name(nameof(Delayed))]
        public Sleepinesses? Delayed { get; set; }
        [Name(nameof (DelayedUtc))] 
        public DateTime? DelayedUtc { get; set; }
        [Name(nameof(Followup))]
        public Sleepinesses? Followup { get; set; }
        [Name(nameof (FollowupUtc))] 
        public DateTime? FollowupUtc { get; set; }
        
        public sealed class Map : ClassMap<StanfordCsvModel>
        {
            public Map()
            {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID)).Index(0);
                Map(m => m.Immediate).Name(nameof(Immediate)).TypeConverter<CsvSleepinessesConverter>().Index(1);
                Map(m => m.ImmediateUtc).Name(nameof(ImmediateUtc)).Index(2);
                Map(m => m.Delayed).Name(nameof(Delayed)).TypeConverter<CsvSleepinessesConverter>().Index(3);
                Map(m => m.DelayedUtc).Name(nameof(DelayedUtc)).Index(4);
                Map(m => m.Followup).Name(nameof(Followup)).TypeConverter<CsvSleepinessesConverter>().Index(5);
                Map(m => m.FollowupUtc).Name(nameof(FollowupUtc)).Index(6);
            }
        }
    }
}