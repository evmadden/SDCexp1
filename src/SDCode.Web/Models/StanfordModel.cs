using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class StanfordModel
    {
        [Name(nameof(ParticipantID))]
        [Description("ID of the participant.")]
        public string ParticipantID { get; set; }

        [Name(nameof (Immediate))] 
        public Sleepinesses? Immediate { get; set; }
        [Name(nameof(Delayed))]
        public Sleepinesses? Delayed { get; set; }
        [Name(nameof(Followup))]
        public Sleepinesses? Followup { get; set; }
    }   
    public sealed class StanfordMap : ClassMap<StanfordModel>
    {
        public StanfordMap()
        {
            Map(m => m.ParticipantID).Name(nameof(StanfordModel.ParticipantID));
            Map(m => m.Immediate).Name(nameof(StanfordModel.Immediate)).TypeConverter<CsvSleepinessesConverter>();
            Map(m => m.Delayed).Name(nameof(StanfordModel.Delayed)).TypeConverter<CsvSleepinessesConverter>();
            Map(m => m.Followup).Name(nameof(StanfordModel.Followup)).TypeConverter<CsvSleepinessesConverter>();
        }
    }
}
