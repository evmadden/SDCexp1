using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    public class StanfordModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }

        [Name(nameof (Immediate))] 
        public Sleepinesses? Immediate { get; set; }
        [Name(nameof(Delayed))]
        public Sleepinesses? Delayed { get; set; }
        [Name(nameof(FollowUp))]
        public Sleepinesses? FollowUp { get; set; }
    }   
    public sealed class StanfordMap : ClassMap<StanfordModel>
    {
        public StanfordMap()
        {
            Map(m => m.ParticipantID).Name(nameof(StanfordModel.ParticipantID));
            Map(m => m.Immediate).Name(nameof(StanfordModel.Immediate)).TypeConverter<CsvSleepinessesConverter>();
            Map(m => m.Delayed).Name(nameof(StanfordModel.Delayed)).TypeConverter<CsvSleepinessesConverter>();
            Map(m => m.FollowUp).Name(nameof(StanfordModel.FollowUp)).TypeConverter<CsvSleepinessesConverter>();
        }
    }
}
