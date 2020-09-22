using System.Collections.Generic;
using System.ComponentModel;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SDCode.Web.Classes;

namespace SDCode.Web.Models
{
    [Description("Images used in each phase.")]
    public class PhaseSetsModel
    {
        [Name(nameof(ParticipantID))]
        public string ParticipantID { get; set; }
        [Name(nameof(Encoding))]
        public IEnumerable<string> Encoding { get; set; }
        [Name(nameof(Immediate))]
        public IEnumerable<string> Immediate { get; set; }
        [Name(nameof(Delayed))]
        public IEnumerable<string> Delayed { get; set; }
        [Name(nameof(Followup))]
        public IEnumerable<string> Followup { get; set; }

        public sealed class Map : ClassMap<PhaseSetsModel> {
            public Map() {
                Map(m => m.ParticipantID).Name(nameof(ParticipantID));
                Map(m => m.Encoding).Name(nameof(Encoding)).TypeConverter<CsvStringsConverter>();
                Map(m => m.Immediate).Name(nameof(Immediate)).TypeConverter<CsvStringsConverter>();
                Map(m => m.Delayed).Name(nameof(Delayed)).TypeConverter<CsvStringsConverter>();
                Map(m => m.Followup).Name(nameof(Followup)).TypeConverter<CsvStringsConverter>();
            }
        }
    }
}
